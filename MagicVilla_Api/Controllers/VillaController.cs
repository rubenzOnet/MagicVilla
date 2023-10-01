using Magic_Villa_Core.Response;
using MagicVilla_Api.Datos;
using MagicVilla_Api.Models;
using MagicVilla_Api.Models.DTO;
using MagicVilla_Api.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MagicVilla_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaController : ControllerBase
    {

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public Response GetVillas()
        {
            var resultado = VillaStore.villaList;
           
            return resultado.ToResponse(true, HttpStatusCode.OK, "");
        }

        [HttpGet("id:int")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //public ActionResult<VillaDTO> GetVilla(int id) 
        public Response GetVilla(int id) 
        {

            VillaDTO villaDTOResponse = new VillaDTO();
            try
            {
                if (id == 0)
                {
                    return villaDTOResponse.ToResponse(false, HttpStatusCode.BadRequest, "Algo salio mal.");
                }

                villaDTOResponse = VillaStore.villaList.FirstOrDefault(w => w.Id == id);

                
                if (villaDTOResponse == null)
                {
                    return villaDTOResponse.ToResponse(false, HttpStatusCode.NotFound, "No se econtró la villa");
                }

                return villaDTOResponse.ToResponse(true, HttpStatusCode.OK, string.Empty);
            }
            catch (Exception ex)
            {
                return villaDTOResponse.ToResponse(false, HttpStatusCode.InternalServerError, ex.Message);
            }

        }



        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Response CreateVilla([FromBody] VillaRequest villaRequest)
        {
            VillaDTO villaDTO = new VillaDTO();

            try
            {
                if (villaRequest == null)
                    return villaDTO.ToResponse(false, HttpStatusCode.BadRequest, "Algo salió mal.");

                int idNew = VillaStore.villaList.OrderByDescending(w => w.Id).FirstOrDefault().Id + 1;

                villaDTO.Id = idNew;
                villaDTO.Nombre = villaRequest.Nombre;

                VillaStore.villaList.Add(villaDTO);

                return villaDTO.ToResponse(true, HttpStatusCode.OK, string.Empty);

            }
            catch (Exception ex)
            {
                return villaDTO.ToResponse(false, HttpStatusCode.InternalServerError, ex.Message);
            }
        }

    }
}
