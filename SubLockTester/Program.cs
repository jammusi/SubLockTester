using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Security.Cryptography;
using Common.Services;
using Newtonsoft.Json.Converters;
using System.Threading;
using Common.Model.Enums;
using static System.Net.Mime.MediaTypeNames;
using System.Runtime.ExceptionServices;
using Kraken_Futures.Services.Managers;
using _Margin = Kraken_Margin.Model.Classes;
using Newtonsoft.Json.Linq;
using System.Net.Configuration;
using static System.Net.WebRequestMethods;
using System.Runtime.CompilerServices;
using Common.Model.Order;
using System.Globalization;
using Kraken_Margin.Model.Classes.Order;
using ConsoleApp1.Lock;

namespace ConsoleApp1
{
    internal class Program
    {

        private static object _locker = new object();

        static void Main(string[] args)
        {
            var tester = new LockTester();
            tester.Run();
            return;

            


            var dateTimeStr = "2023-11-09T03:00:18.039Z";

            DateTime parseDT = DateTime.MinValue;

            DateTime.TryParse(dateTimeStr, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal, out parseDT);

            Console.WriteLine(dateTimeStr);
            Console.WriteLine(parseDT);

            return;


            
        }
        
    }
}
