using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eightmulator
{
    public static class Debugger
    {
        public static bool Debug = true;

        public static void WriteLine(string str, object[] param)
        {
            if(Debug)
            {
                Console.WriteLine(str, param);
            }
        }

        public static void WriteLine(string str)
        {
            if (Debug)
            {
                Console.WriteLine(str);
            }
        }

        public static void WriteLine(string str, object param)
        {
            if (Debug)
            {
                Console.WriteLine(str, param);
            }
        }

        public static void WriteLine(string str, object arg0, object arg1, object arg2)
        {
            if (Debug)
            {
                Console.WriteLine(str, arg0, arg1,arg2);
            }
        }
    }
}
