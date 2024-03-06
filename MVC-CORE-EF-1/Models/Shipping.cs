using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_CORE_EF_1.Models
{
    public class Shipping
    {
        [Key]
        public int IdSpedizione { get; set; }
        [ForeignKey("User")]
        [Required]
        public int IdUser { get; set; }
        [Required]
        public DateTime DataSpedizione { get; set; }
        [Required]
        public double PesoSpedizione { get; set; }
        [Required]
        public string IndirizzoDestinatario { get; set; }
        [Required]
        public string NominativoDestinatario { get; set; }
        [Required]
        public double CostoSpedizione { get; set; }
        [Required]
        public DateTime DataConsegna { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<ShippingDetail> ShippingDetails { get; set; }

    }
}
