using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeDirectMicroservice.Models
{
    public class ExchangeTrade
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        public double AmountToTrade { get; set; }

        //public virtual IEnumerable<Currency> Currencies { get; set; }

        //[ForeignKey("CurrencySymbol")]
        [Required]
        public string CurrencyFrom { get; set; }

        //public virtual Currency ConvertFrom { get; set; }

        //[ForeignKey("CurrencySymbol")]
        [Required]
        public string CurrencyTo { get; set; }

        [Required]
        public double TradedAmount { get; set; }

        //public virtual Currency ConvertTo { get; set; }

        [Required]
        public DateTime ExchangeTime { get; set; }

        [Required]
        public string UserName { get; set; }
    }
}
