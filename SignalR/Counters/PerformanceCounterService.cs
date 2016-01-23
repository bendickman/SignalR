using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalR.Counters
{
    public class PerformanceCounterService
    {
        List<PerformanceCounterWrapper> _counters;

        public PerformanceCounterService()
        {
            _counters = new List<PerformanceCounterWrapper>();

            _counters.Add(new PerformanceCounterWrapper("Processor",
                "Processor", "% Processor Time", "_Total"));

            _counters.Add(new PerformanceCounterWrapper("Paging",
                "Memory", "Pages/sec"));

            _counters.Add(new PerformanceCounterWrapper("Disk",
                "PhysicalDisk", "% Disk Time", "_Total"));
        }

        public dynamic GetResults()
        {
            return _counters.Select(c => new
            { name = c.Name, value = c.Value });
        }
    }
}