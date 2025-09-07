using System;

namespace HospitalManagementApplication.Util{

    public static class Util{

        public static void Pause(Action action){
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            action();
        }
        public static void Pause(){
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }

}