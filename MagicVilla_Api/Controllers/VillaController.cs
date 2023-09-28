using MagicVilla_Api.Datos;
using MagicVilla_Api.Models;
using MagicVilla_Api.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaController : ControllerBase
    {

        [HttpGet]
        public IEnumerable<VillaDTO> GetVillas()
        {
            return VillaStore.villaList;
        }

        [HttpGet("id:int")]
        public VillaDTO GetVilla(int id) 
        {
            return VillaStore.villaList.FirstOrDefault(w => w.Id == id);
        }





    }
}
