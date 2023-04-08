using System.ComponentModel.DataAnnotations;

namespace MeDirectMicroservice.Models
{
    public class Currency
    {
        [Key]
        [Required]
        public string CurrencySymbol { get; set; }
        
        [Required]
        public string CountryName { get; set; }

        //public virtual IEnumerable<ExchangeTrade> ExchangeTrades { get; set; }
    }
}
