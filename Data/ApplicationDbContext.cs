using MeDirectMicroservice.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MeDirectMicroservice.Data
{
    public class ApplicationDbContext :IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<ExchangeTrade> ExchangeTrades { get; set; }
        public DbSet<Currency> Currencies { get; set; }
    }
}
