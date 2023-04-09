namespace MeDirectMicroservice.DataAccess.Interfaces
{
    public interface ITradeRepository
    {
        bool TradeLimitExceeded(string username);
    }
}
