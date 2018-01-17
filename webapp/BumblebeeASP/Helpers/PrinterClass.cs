using System;
namespace BumblebeeASP.Helpers
{
    public class PrinterClass
    {
        public static void printErrorMessage(string message)
        {
            System.Console.WriteLine("ERROR_MESSAGE: " + message);;
        }

        public static void printDebugMessage(string message)
        {
            System.Console.WriteLine("DEBUG_MESSAGE: " + message);
        }
        public PrinterClass()
        {
        }
    }
}
