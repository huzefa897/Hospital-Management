# Hospital Management (Console) — Publish & Run Guide

This app is a cross-platform C# console application using **.NET**, **EF Core**, and **SQLite**.  
It runs on **Windows** and **macOS** and can be shared as a simple folder or single executable.

---

## Quick Start

1. **Clone** the repo and open a terminal in the project folder.  
2. (Optional) Verify .NET SDK (8 or 9):  
   ```bash
   dotnet --info
   ```
3. **Build & run from source:**
   ```bash
   dotnet restore
   dotnet build
   dotnet run
   ```
4. **Publish self-contained** (no .NET install needed on target machine):
   ```bash
   dotnet publish -c Release -r win-x64   --self-contained true -p:PublishSingleFile=true -p:PublishTrimmed=false -o publish/win-x64
   dotnet publish -c Release -r osx-arm64 --self-contained true -p:PublishSingleFile=true -p:PublishTrimmed=false -o publish/osx-arm64
   ```
5. Zip the folder that matches the user’s OS/CPU and share it. They run the app directly.

---

## Requirements

- **.NET SDK**: 8 (LTS) or 9  
  Check with:
  ```bash
  dotnet --info
  ```
- **NuGet packages** (already referenced in the project — run once if needed):
  ```bash
  dotnet add package Microsoft.EntityFrameworkCore
  dotnet add package Microsoft.EntityFrameworkCore.Sqlite
  dotnet add package Microsoft.EntityFrameworkCore.Design
  dotnet add package SQLitePCLRaw.bundle_e_sqlite3
  dotnet tool install --global dotnet-ef
  ```

> `SQLitePCLRaw.bundle_e_sqlite3` ensures native SQLite works on Windows & macOS.

---

## Database Location (Portable & Safe)

This app stores `hospital.db` in the current user’s application-data folder (writable on both OSes).  
Typical paths:

- **Windows**: `C:\Users\<you>\AppData\Local\HospitalManagementApp\hospital.db`  
- **macOS**: `/Users/<you>/Library/Application Support/HospitalManagementApp/hospital.db`

You can override the location with an environment variable:

- **Windows (CMD)**:
  ```bat
  set HMS_DB_PATH=C:\somewhere\hospital.db && HospitalManagementApplication.exe
  ```
- **macOS (bash/zsh)**:
  ```bash
  HMS_DB_PATH="/somewhere/hospital.db" ./HospitalManagementApplication
  ```

On first launch, the app runs:
```csharp
db.Database.Migrate();
```
so the schema is created/updated automatically.

---

## Run From Source

```bash
dotnet restore
dotnet build
```

Create/apply migrations (first time or after model changes):
```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

Run:
```bash
dotnet run
```

---

## Publish Options

### A) Self-Contained (Recommended)
No .NET install required on the target machine. Produce one folder per OS/CPU.

```bash
# Windows (Intel/AMD PCs)
dotnet publish -c Release -r win-x64 --self-contained true \
  -p:PublishSingleFile=true -p:PublishTrimmed=false -o publish/win-x64

# Windows (ARM PCs)
dotnet publish -c Release -r win-arm64 --self-contained true \
  -p:PublishSingleFile=true -p:PublishTrimmed=false -o publish/win-arm64

# macOS Intel
dotnet publish -c Release -r osx-x64 --self-contained true \
  -p:PublishSingleFile=true -p:PublishTrimmed=false -o publish/osx-x64

# macOS Apple Silicon (M1/M2/M3)
dotnet publish -c Release -r osx-arm64 --self-contained true \
  -p:PublishSingleFile=true -p:PublishTrimmed=false -o publish/osx-arm64
```

> We set `PublishTrimmed=false` to avoid trimming EF Core migration bits.

### B) Framework-Dependent (Smaller Download)
Requires the .NET **runtime** on the target machine.

```bash
dotnet publish -c Release -o publish/portable
```

Run with:
```bash
dotnet HospitalManagementApplication.dll
```

---

## How End Users Run It

### Windows
- **Self-contained**: double-click `HospitalManagementApplication.exe` inside `publish\win-x64` (or `win-arm64`).  
  If Windows SmartScreen warns: **More info → Run anyway** (normal for unsigned apps).

- **Framework-dependent**: install the matching .NET runtime (8 or 9), then:
  ```bat
  cd publish\portable
  dotnet HospitalManagementApplication.dll
  ```

### macOS
- **Self-contained**:
  ```bash
  cd /path/to/publish/osx-arm64   # or osx-x64 for Intel Macs
  chmod +x ./HospitalManagementApplication   # first time only
  ./HospitalManagementApplication
  ```
  If Gatekeeper warns about unidentified developer, right-click → **Open** → **Open** once, or run from Terminal as above.

- **Framework-dependent**: install .NET runtime, then:
  ```bash
  cd /path/to/publish/portable
  dotnet HospitalManagementApplication.dll
  ```

---

## Updating / Uninstalling

- **Update**: replace the old published folder with the new one. Your data persists (DB is in the user’s app-data folder).  
- **Uninstall**: delete the app folder. Optionally delete the DB folder listed in [Database Location](#database-location-portable--safe).

---

## Troubleshooting

- **“no such table: …”**  
  Migrations weren’t applied or it’s a new DB path. Keep `db.Database.Migrate()` at startup.  
  You can also reapply:
  ```bash
  dotnet ef database update
  ```

- **“.NET hostfxr / runtime not found”**  
  Use a **self-contained** build, or install the matching .NET runtime (8/9) and use the portable publish.

- **SQLite load errors**  
  Ensure `SQLitePCLRaw.bundle_e_sqlite3` is referenced. Rebuild/publish.

- **macOS can’t open app (unidentified developer)**  
  Right-click → **Open** once, or run from Terminal (`chmod +x` first time).

- **Windows SmartScreen warning**  
  Click **More info → Run anyway** (normal for unsigned classroom apps).

---

## Extras

### Build All Targets Script
Create `build-all.sh` (macOS/Linux):

```bash
#!/usr/bin/env bash
set -euo pipefail
dotnet restore
for rid in win-x64 win-arm64 osx-x64 osx-arm64; do
  out="publish/$rid"
  dotnet publish -c Release -r "$rid" --self-contained true \
    -p:PublishSingleFile=true -p:PublishTrimmed=false -o "$out"
  echo "Built -> $out"
done
```

Run:
```bash
chmod +x build-all.sh
./build-all.sh
```

### Optional GitHub Actions Workflow
Add `.github/workflows/publish.yml` to build artifacts on every push to `main`:

```yaml
name: Publish Cross-Platform Binaries
on:
  push:
    branches: [ main ]

jobs:
  build:
    runs-on: ubuntu-latest
    strategy:
      matrix:
        rid: [ win-x64, win-arm64, osx-x64, osx-arm64 ]
    steps:
      - uses: actions/checkout@v4
      - uses: actions/setup-dotnet@v4
        with:
          dotnet-version: '8.0.x'
      - name: Restore
        run: dotnet restore
      - name: Publish ${{ matrix.rid }}
        run: |
          dotnet publish -c Release -r ${{ matrix.rid }} --self-contained true \
            -p:PublishSingleFile=true -p:PublishTrimmed=false \
            -o publish/${{ matrix.rid }}
      - name: Upload artifact
        uses: actions/upload-artifact@v4
        with:
          name: hms-${{ matrix.rid }}
          path: publish/${{ matrix.rid }}
```

---

## FAQ

**Do users need to install .NET?**  
No, if you share **self-contained** builds. Yes, if you share the **portable** (framework-dependent) build.

**Where is the database? Can I move it?**  
See [Database Location](#database-location-portable--safe). Override with `HMS_DB_PATH` if needed.

**Which publish folder do I send?**  
Match the device:
- Most PCs → `win-x64`
- ARM Windows → `win-arm64`
- Apple Silicon Mac → `osx-arm64`
- Intel Mac → `osx-x64`
