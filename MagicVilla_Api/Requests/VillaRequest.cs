using System.ComponentModel.DataAnnotations;

namespace MagicVilla_Api.Requests
{
    public class VillaRequest
    {
        [Required(ErrorMessage = "El campo nombre es requerido.")]
        [MaxLength(30)]
        public string Nombre { get; set; }
        public int Ocupantes { get; set; }
        public int MetrosCuadrados { get; set; }

    }
}
