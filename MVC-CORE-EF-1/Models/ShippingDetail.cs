using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_CORE_EF_1.Models
{
    public class ShippingDetail
    {
        [Key]
        public int IdShippingDetail { get; set; }
        [Required]
        [ForeignKey("Shipping")]
        public int IdSpedizione { get; set; }
        [Required]
        public string? StatoSpedizione { get; set; }
        [Required]
        public string? LuogoCorrente { get; set; }
        [Required]
        public string? NoteSpedizione { get; set; }
        [Required]
        public DateTime DataAggiornamento { get; set; }

        public virtual Shipping Shipping { get; set; }
    }
}
