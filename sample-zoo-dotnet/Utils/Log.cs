using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sample_zoo_dotnet.Utils
{
    // this is dummy logger, please use a real logging solution
    public class Log
    {
        public static void Warning(string message)
        {
            System.Console.WriteLine(message);
        }

        public static void Warning(Exception e)
        {
            Warning(e.Message);
        }

        public static void Error(string message)
        {
            System.Console.Error.WriteLine(message);
        }

        public static void Error(Exception e)
        {
            Error(e.Message);
        }
    }
}