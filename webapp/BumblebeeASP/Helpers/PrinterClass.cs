using System;
using System.Diagnostics;
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
    }
}
