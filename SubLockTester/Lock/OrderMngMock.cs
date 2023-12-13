using Common.Model;
using Common.Model.Enums;
using Common.Services;
using Kraken_Futures.Services.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1.Lock
{
    public class OrderMngMock_
    {
        private object _isSubscribeToTradeUpdatesLocker = new object();
        private object _isSubscribeToOrderUpdatesLocker = new object();

        private bool _isSubscribedToOrdersUpdates = false;
        private bool _isSubscribedToTradeUpdates = false;

        #region PROPERTIES
        
        private bool IsSub_Orders2
        {
            get
            {
                    return _isSubscribedToOrdersUpdates;
            }
            set
            {
                    _isSubscribedToOrdersUpdates = value;
            }
        }
        private bool IsSub_Orders
        {
            get
            {
                var v = false;

                _Log("GET_IsSub_Orders", "prelock");
                lock (_isSubscribeToOrderUpdatesLocker)
                {
                    v = _isSubscribedToOrdersUpdates;
                }
                _Log("GET_IsSub_Orders", "lock-released");

                return v;
            }
            set
            {
                _Log("SET_IsSub_Orders", "prelock");
                lock (_isSubscribeToOrderUpdatesLocker)
                {
                    _isSubscribedToOrdersUpdates = value;
                }
                _Log("SET_IsSub_Orders", "lock-released");

            }
        }
        
        private bool IsSub_Trades2
        {
            get
            {
                    return _isSubscribedToTradeUpdates;
            }
            set
            {
                    _isSubscribedToTradeUpdates = value;
            }
        }
        private bool IsSub_Trades
        {
            get
            {
                var v = false;
                
                _Log("GET_IsSub_Trades", "prelock");

                lock (_isSubscribeToTradeUpdatesLocker)
                {
                    v = _isSubscribedToTradeUpdates;
                }

                _Log("GET_IsSub_Trades", "lock-released");

                return v;

            }
            set
            {
                _Log("SET_IsSub_Trades", "prelock");
                lock (_isSubscribeToTradeUpdatesLocker)
                {
                    _isSubscribedToTradeUpdates = value;
                }
                _Log("SET_IsSub_Trades", "lock-released");

            }
        }
        
        #endregion

        public void GetTrades()
        {
            var name = "GetTrades";
            var log = $"{name} called - ";
            var log2 = "and skipped";
            var exLog = $"";
            _Log($"{name}", "Started");

            lock (_isSubscribeToTradeUpdatesLocker)
            {
                try
                {
                    log2 = $"and activated. IsSub_Trades WAS:{IsSub_Trades}";

                    #region DEBUGGING LOG
                    _SubsStateLog($"{name}");
                    #endregion

                    if (!IsSub_Trades)
                    {
                        IsSub_Trades = true;
                        _DoGetTrades();
                    }
                }
                catch (Exception e)
                {
                    exLog = $" **Exception occured!!";
                    _Log($"{name}", e);
                }
            }

            var log3 = $"Exit report: {log}{log2}{exLog}";
            _Log($"{name}", log3);
        }

        public void GetOrders()
        {
            var name = "GetOrders";
            var log = $"{name} called - ";
            var log2 = "and skipped";
            var exLog = $"";
            _Log($"{name}", "Started");

            lock (_isSubscribeToOrderUpdatesLocker)
            {
                try
                {
                    log2 = $"and activated. IsSub_Orders WAS:{IsSub_Orders}";

                    #region DEBUGGING LOG
                    _SubsStateLog($"{name}");
                    #endregion

                    if (!IsSub_Orders)
                    {
                        IsSub_Orders = true;
                        _DoGetOrders();
                    }
                }
                catch (Exception ex)
                {
                    exLog = $" **Exception occured!!";
                    _Log($"{name}", ex);
                }
            }

            var log3 = $"Exit report: {log}{log2}{exLog}";
            _Log($"{name}", log3);
        }

        private void _DoGetTrades()
        {
            _Log("_DoSubGet-Trades", "");
        }

        private void _DoGetOrders()
        {
            _Log("_DoSubGet-Orders", "");
        }

        public static void _Log(string funcName, string msg, bool iserr = false)
        {
            var msg_ = $"{funcName}: {msg}";
            Logger.Write(msg_, iserr);

            var treadid = Thread.CurrentThread.ManagedThreadId;
            var consoleMsg = $"Thread: {treadid}: {msg_}";
            Console.WriteLine(consoleMsg);
        }

        private void _Log(string funcName, Exception e)
        {
            _Log(funcName,$"exception: {e}", true);
        }

        private void _SubsStateLog(string callingFunction, string additionalInfo = "")
        {
            //return;

            var logMsg = $" {additionalInfo} Subscription Status Log: ";
            logMsg += $"IsSub_Orders:{IsSub_Orders}";
            logMsg += $", IsSub_Trades:{IsSub_Trades}";
            _Log(callingFunction, logMsg);
        }
    }
}
