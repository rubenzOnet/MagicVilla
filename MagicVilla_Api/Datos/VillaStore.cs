using MagicVilla_Api.Models.DTO;

namespace MagicVilla_Api.Datos
{
    public static class VillaStore
    {
        public static List<VillaDTO> villaList = new List<VillaDTO>
        {
            new VillaDTO {Id = 1, Nombre = "Vista a la piscina"},
            new VillaDTO {Id = 2, Nombre = "Vista a la playa"}

        };
    }
}
