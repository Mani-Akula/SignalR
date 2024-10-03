using System.ComponentModel.DataAnnotations;

namespace RealTimeCharts.Server.Models
{
    public class ChartModel
    {
        [Key]
        public string Data { get; set; }
        public string Label { get; set; }
        public string BackgroundColor { get; set; }

    }
}
