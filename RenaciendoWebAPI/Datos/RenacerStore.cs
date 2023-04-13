using RenaciendoWebAPI.Models.Dtos;

namespace RenaciendoWebAPI.Datos
{
    public static class RenacerStore
    {
        public static List<RenacerDTO> renacerList = new()
        {
            new RenacerDTO { Nombre = "Nacimiento", RenacerId = 1},
                new RenacerDTO { Nombre = "Desarrollo", RenacerId = 2},
                new RenacerDTO { Nombre = "Más Crecimiento", RenacerId = 3}
        };
    }
}
