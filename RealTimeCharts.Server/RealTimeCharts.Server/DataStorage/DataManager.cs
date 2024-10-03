using Microsoft.EntityFrameworkCore;
using RealTimeCharts.Server.Data;
using RealTimeCharts.Server.Models;

namespace RealTimeCharts.Server.DataStorage
{
    public class DataManager
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public DataManager(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }
        public List<ChartModel> GetData()
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<LoansDbContext>();


            var chartDataEntities = context.ChartData
                                        .FromSqlRaw("EXEC DataGet")
                                        .ToList();

            var data = chartDataEntities.Select(x => new ChartModel
            {
                Data = x.Data,
                Label = x.Label,
                BackgroundColor = x.BackgroundColor
            }).ToList();

          
            return data;
        }

    }

    
}