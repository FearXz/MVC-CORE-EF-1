using System.ComponentModel.DataAnnotations;

namespace MVC_CORE_EF_1.Models
{
    public class User
    {
        [Key]
        public int IdUser { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Nominativo { get; set; }
        [Required]
        public string Role { get; set; } = UserRole.USER;
        [Required]
        public string TipoCliente { get; set; }
        public string? CodiceFiscale { get; set; }
        public string? PartitaIva { get; set; }
        public virtual ICollection<Shipping> Shippings { get; set; }
    }
}

