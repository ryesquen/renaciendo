using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RenaciendoWebAPI.Models
{
    public class Renacer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RenacerId { get; set; }
        public string? Nombre { get; set; }
        public string? Detalle { get; set; }
        public int Dimension { get; set; }
        public string? ImageURL { get; set; }
        public string? Amenidad { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaActualizacion { get; set; }
    }
}
