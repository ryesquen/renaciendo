using System.ComponentModel.DataAnnotations;

namespace RenaciendoWebAPI.Models.Dtos
{
    public class RenacerDTO
    {
        public int RenacerId { get; set; }
        [Required]
        [MaxLength(30)]
        public string? Nombre { get; set; }
    }
}
