using System.ComponentModel.DataAnnotations;

namespace RenaciendoWebAPI.Models.Dtos
{
    public class RenacerDTO
    {
        public int RenacerId { get; set; }
        public string? Nombre { get; set; }
        public string? Detalle { get; set; }
        public string? Razon { get; set; }
        public int Dimension { get; set; }
        public string? ImageURL { get; set; }
        public string? Amenidad { get; set; }
    }
}
