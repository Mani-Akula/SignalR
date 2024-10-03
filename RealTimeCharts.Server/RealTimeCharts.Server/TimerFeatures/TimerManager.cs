using Microsoft.AspNetCore.SignalR;
using RealTimeCharts.Server.DataStorage;
using RealTimeCharts.Server.HubConfig;

namespace RealTimeCharts.Server.TimerFeatures
{
    public class TimerManager
    {
        private Timer? _timer;
        private AutoResetEvent? _autoResetEvent;
        private IHubContext<ChartHub> _hub;
        private DataManager _dataManager;
        public DateTime TimerStarted { get; set; }
        public bool IsTimerStarted { get; set; }

        public TimerManager(IHubContext<ChartHub> hub, DataManager dataManager)
        {
            _hub = hub;
            _dataManager = dataManager;
        }
        public void PrepareTimer()
        {
            _autoResetEvent = new AutoResetEvent(false);
            _timer = new Timer(Execute, _autoResetEvent, 1000, 2000);
            TimerStarted = DateTime.Now;
            IsTimerStarted = true;
        }

        public void Execute(object? stateInfo)
        {
            var data =  _dataManager.GetData();
            _hub.Clients.All.SendAsync("TransferChartData", data);

            if ((DateTime.Now - TimerStarted).TotalSeconds > 60)
            {
                IsTimerStarted = false;
                _timer.Dispose();
            }
        }
    }
}
