using System;
using MeDirectMicroservice.Data;
using MeDirectMicroservice.DataAccess.Interfaces;

namespace MeDirectMicroservice.DataAccess.Repositories
{
    public class TradeRepository: ITradeRepository
    {
        private readonly ApplicationDbContext _context;

        public TradeRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool TradeLimitExceeded(string username)
        {
            var trades = from t in _context.ExchangeTrades.ToList()
                         where t.UserName == username && (DateTime.Now - t.ExchangeTime).TotalMinutes < 60
                         select t;

            if (trades.Count() > 9)
                return true;
            else
                return false;
        }
    }
}
