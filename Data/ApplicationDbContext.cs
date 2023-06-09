﻿using MeDirectMicroservice.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MeDirectMicroservice.Data
{
    public class ApplicationDbContext :IdentityDbContext//<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        public DbSet<ExchangeTrade> ExchangeTrades { get; set; }
        public DbSet<Currency> Currencies { get; set; }
    }
}
