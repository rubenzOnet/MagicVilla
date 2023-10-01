using Magic_Villa_Core.Response;
using MagicVilla_Api.Datos;
using MagicVilla_Api.Models;
using MagicVilla_Api.Models.DTO;
using MagicVilla_Api.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
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

        [HttpGet("id:int", Name = "GetVilla")]
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
                if (!ModelState.IsValid)
                    return villaDTO.ToResponse(false, HttpStatusCode.BadRequest, "Error en el modelo");

                if (villaRequest == null)
                    return villaDTO.ToResponse(false, HttpStatusCode.BadRequest, "Algo salió mal.");

                if (VillaStore.villaList.FirstOrDefault(w => w.Nombre.ToLower() == villaRequest.Nombre.ToLower()) != null)
                {
                    // ModelState.AddModelError("NombreExiste", "La villa ya existe.");
                    // return ModelState.ToResponse(false, HttpStatusCode.BadRequest, "");
                    return villaDTO.ToResponse(false, HttpStatusCode.BadRequest, "La villa ya existe.");
                }

                int idNew = VillaStore.villaList.OrderByDescending(w => w.Id).FirstOrDefault().Id + 1;

                villaDTO.Id = idNew;
                villaDTO.Nombre = villaRequest.Nombre;

                VillaStore.villaList.Add(villaDTO);

                return villaDTO.ToResponse(true, HttpStatusCode.OK, string.Empty);

                //return CreatedAtRoute("GetVilla", new { id = villaDTO.Id }, villaDTO);

            }
            catch (Exception ex)
            {
                return villaDTO.ToResponse(false, HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpDelete("id:int")]
        public Response DeleteVilla(int id)
        {
            VillaDTO villaDTO = new VillaDTO();

            try
            {
                if (id == 0)
                    return villaDTO.ToResponse(false, HttpStatusCode.BadRequest, "El id en cereo no es correcto.");


                villaDTO = VillaStore.villaList.FirstOrDefault( w => w.Id  == id);
                if(villaDTO == null)
                    return villaDTO.ToResponse(false, HttpStatusCode.NotFound, "El id en no existe.");

                VillaStore.villaList.Remove(villaDTO);

                return villaDTO.ToResponse(true, HttpStatusCode.OK, "La villa se ha borrado.");
            }
            catch (Exception ex)
            {
                return villaDTO.ToResponse(false, HttpStatusCode.InternalServerError, ex.Message);
            }
        }


        [HttpPut("id:int")]
        public Response UpdateVilla([FromBody] VillaDTO villaDTO, int id)
        {

            if (villaDTO == null || id != villaDTO.Id)
                return villaDTO.ToResponse(false, HttpStatusCode.BadRequest, "Datos erroneos.");

            var villa = VillaStore.villaList.FirstOrDefault(w => w.Id == id);
            villa.Nombre = villaDTO.Nombre;
            villa.Ocupantes = villaDTO.Ocupantes;
            villa.MetrosCuadrados = villaDTO.MetrosCuadrados;

            return villaDTO.ToResponse(true, HttpStatusCode.OK, "Datos de villa actualizados.");
        }


        [HttpPatch("id:int")]
        public Response UpdatePartialVilla(int id, JsonPatchDocument<VillaDTO> patchDTO)
        {

            if (patchDTO == null || id == 0)
                return patchDTO.ToResponse(false, HttpStatusCode.BadRequest, "Datos erroneos.");

            
            var villa = VillaStore.villaList.FirstOrDefault(w => w.Id == id);

            patchDTO.ApplyTo(villa, ModelState);

            if(!ModelState.IsValid)
                return patchDTO.ToResponse(false, HttpStatusCode.BadRequest, "Datos erroneos.");

            return patchDTO.ToResponse(true, HttpStatusCode.OK, "Datos de villa actualizados.");
        }




    }
}
