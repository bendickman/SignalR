using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;
using SignalR.Counters;
using System.Timers;

namespace SignalR.Hubs
{
    //public class ClientPushHub : Hub
    //{
    //}

    public class PerfHub : Hub
    {
        //private IHubContext hub;
        private Timer _timer;

        public PerfHub()
        {
            StartCounterCollection();
            PushNotification("", 10000);
            //hub = GlobalHost.ConnectionManager.GetHubContext<ClientPushHub>();
        }

        

        private void StartCounterCollection()
        {
            var task = Task.Factory.StartNew(async () =>
            {
                var perfService = new PerformanceCounterService();
                //infinite loop
                while (true)
                {
                    var results = perfService.GetResults();
                    Clients.All.newCounters(results);
                    await Task.Delay(2000);
                }
            }, TaskCreationOptions.LongRunning);
            
        }

        public void Send(string message)
        {
            Clients.All.newMessage(message + Environment.MachineName);
        }
        
        public void PushNotification(string message, double interval)
        {
            _timer = new Timer();

            _timer.Elapsed += new ElapsedEventHandler(TimerEvent);
            _timer.Interval = interval;
            _timer.Enabled = true;
        }

        private void TimerEvent(object source, ElapsedEventArgs e)
        {
            Send("timed push notification from: ");
            //Clients.All.notification("Timed push notification!");
        }

        
    }
}