using Common.Model;
using Common.Model.Enums;
using Common.Services;
using Kraken_Futures.Services.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Lock
{
    public class LockTester
    {
        //private OrderMngMock mng = new OrderMngMock();
        private LockUser mng = new LockUser();

        public LockTester()
        {
            //mng = new OrderMngMock();
        }
        
        public void Run()
        {
            Logger.Init("LockTester");
            var again = true;

            var callsNum = 5;

            callsNum = _getNumOfCalls();

            while (again)
            {

                _Log($"Tester.Run",$" executing {callsNum} times");

                #region CALL SUB ON NEW THEARDS
                var tasks = new List<Task>();
                for (var i = 0; i < callsNum; i++)
                {
                    var t = Task.Run(() => _Subscribe());

                    tasks.Add(t);
                }

                Task.WaitAll(tasks.ToArray());

                #endregion

                #region AGAIN?
                var input = _ReadInput("Again(any key to stop)");
                again = string.IsNullOrEmpty(input);

                if (again)
                {
                    callsNum = _getNumOfCalls();
                    _Log("Tester.Run ", "running again***");
                } 
                #endregion
            }

            #region INTERNAL FUNC
            int _getNumOfCalls()
            {
                string input = _ReadInput($"Num of calls ({callsNum} defult): ");

                var callsNum_ = string.IsNullOrEmpty(input) ? callsNum : Convert.ToInt16(input);

                return callsNum_;
            }

            #endregion
        }

        private static string _ReadInput(string msg)
        {
            Console.Write($"{msg}");
            var input = Console.ReadLine();
            return input;
        }

        private void _Subscribe()
        {
            _Log("Tester._Subscribe", "Starts");

            mng.GetTrades();
            mng.GetOrders();
         
            _Log("Tester._Subscribe", "End");
        }

        private void _Log(string funcName, string msg)
        {
            LockUser._Log(funcName, msg);
        }
    }
}
