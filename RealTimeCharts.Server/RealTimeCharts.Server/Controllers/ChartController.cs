using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using RealTimeCharts.Server.DataStorage;
using RealTimeCharts.Server.HubConfig;
using RealTimeCharts.Server.TimerFeatures;

namespace RealTimeCharts.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChartController : ControllerBase
    {
        private readonly TimerManager _timer;
        
        public ChartController(TimerManager timer)
        {
            _timer = timer;
        }

        [HttpGet]
        public IActionResult Get()
        {
            if (!_timer.IsTimerStarted)
                _timer.PrepareTimer();
            return Ok(new { Message = "Request Completed" });
        }
    }
}
