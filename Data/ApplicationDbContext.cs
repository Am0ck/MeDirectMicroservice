using MeDirectMicroservice.Models;
using Microsoft.EntityFrameworkCore;

namespace MeDirectMicroservice.Data
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<ExchangeTrade> ExchangeTrades { get; set; }
        public DbSet<Currency> Currencies { get; set; }
    }
}
