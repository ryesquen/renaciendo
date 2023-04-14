using RenaciendoWebAPI.Models.Dtos;

namespace RenaciendoWebAPI.Datos
{
    public static class RenacerStore
    {
        public static List<RenacerDTO> renacerList = new()
        {
            new RenacerDTO { Nombre = "Nacimiento", RenacerId = 1, Razon = "Crecer", Dimension = 11},
                new RenacerDTO { Nombre = "Desarrollo", RenacerId = 2, Razon = "sobresalir", Dimension = 12},
                new RenacerDTO { Nombre = "Más Crecimiento", RenacerId = 3, Razon = "Dinero", Dimension = 13}
        };
    }
}
