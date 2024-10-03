using Microsoft.EntityFrameworkCore;
using RealTimeCharts.Server.Models;

namespace RealTimeCharts.Server.Data
{

    public class LoansDbContext : DbContext
    {
        public LoansDbContext(DbContextOptions<LoansDbContext> options) : base(options)
        {
        }

        public DbSet<ChartModel> ChartData { get; set; }
    }
}