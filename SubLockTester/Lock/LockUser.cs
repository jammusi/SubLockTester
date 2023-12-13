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
    public class LockUser
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
                lock (_isSubscribeToOrderUpdatesLocker)
                {
                    return _isSubscribedToOrdersUpdates;
                }
            }
            set
            {
                lock (_isSubscribeToOrderUpdatesLocker)
                {
                    _isSubscribedToOrdersUpdates = value;
                }
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
                lock (_isSubscribeToTradeUpdatesLocker)
                {
                    return _isSubscribedToTradeUpdates;
                }
            }
            set
            {
                lock (_isSubscribeToTradeUpdatesLocker)
                {
                    _isSubscribedToTradeUpdates = value;
                }
            }
        }

        private bool _getIsSubOrders()
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

        private void _setIsSubOrders(bool val)
        {

            _Log("SET_IsSub_Orders", "prelock");
            lock (_isSubscribeToOrderUpdatesLocker)
            {
                _isSubscribedToOrdersUpdates= val;
            }
            _Log("SET_IsSub_Orders", "lock-released");

        }

        private bool _getIsSubTrades()
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

        private void _setIsSubTrades(bool val)
        {

            _Log("SET_IsSub_Trades", "prelock");
            lock (_isSubscribeToTradeUpdatesLocker)
            {
                _isSubscribedToTradeUpdates = val;
            }
            _Log("SET_IsSub_Trades", "lock-released");
            
        }
        #endregion

        public void GetTrades()
        {
            var name = "GetTrades";
            //var log = $"{name} called - ";
            //var log2 = "and skipped";
            
            _Log($"{name}", "Started");

            lock (_isSubscribeToTradeUpdatesLocker)
            {
                try
                {
                    #region DEBUGGING LOG
                    _SubsStateLog($"{name}");
                    #endregion

                    if (!_getIsSubTrades())
                    {
                        _setIsSubTrades(true);
                        //IsSub_Trades = true;
                        _DoGetTrades();
                    }
                }
                catch (Exception e)
                {
                    _Log($"{name}", e);
                }
            }

            _Log($"{name}", "Exit");
        }

        public void GetOrders()
        {
            var name = "GetOrders";
            
            _Log($"{name}", "Started");

            lock (_isSubscribeToOrderUpdatesLocker)
            {
                try
                {
                    #region DEBUGGING LOG
                    _SubsStateLog($"{name}");
                    #endregion

                    if (!_getIsSubOrders())
                    {
                        _setIsSubOrders(true);
                        IsSub_Orders = true;
                        _DoGetOrders();
                    }
                }
                catch (Exception ex)
                {
                    _Log($"{name}", ex);
                }
            }

            _Log($"{name} - Exit", "Exit");
        }

        private void _DoGetTrades()
        {
            _Log("_DoSubGet-Trades", "");
        }

        private void _DoGetOrders()
        {
            _Log("_DoSubGet-Orders", "");
        }

        public static void _Log(string funcName, string msg)
        {
            var msg_ = $"{funcName}: {msg}";
            var treadid = Thread.CurrentThread.ManagedThreadId;
            var consoleMsg = $"Thread: {treadid}: {msg_}";
            
            Console.WriteLine(consoleMsg);
        }

        private void _Log(string funcName, Exception e)
        {
            _Log(funcName,$"exception: {e}");
        }

        private void _SubsStateLog(string callingFunction, string additionalInfo = "")
        {
            //return;

            var logMsg = $" {additionalInfo} Subscription Status Log: ";
            logMsg += $"IsSub_Orders:{_getIsSubOrders()}";
            logMsg += $", IsSub_Trades:{_getIsSubTrades()}";
            _Log(callingFunction, logMsg);
        }

    }
}
