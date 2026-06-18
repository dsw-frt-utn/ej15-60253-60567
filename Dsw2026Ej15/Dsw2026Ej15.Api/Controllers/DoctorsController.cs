using Dsw2026Ej15.Api.Models;
using Dsw2026Ej15.Data;
using Dsw2026Ej15.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Dsw2026Ej15.Api.Controllers
{
    [ApiController]
    [Route("api/doctors")]
    public class DoctorsController : ControllerBase
    {
        private readonly IPersistence _persistence;

        public DoctorsController(IPersistence persistence)
        {
            _persistence = persistence;
        }

        //endpoint 1
        //debemos hacer los metodos del controlador asincronicos
        [HttpPost]
        public async Task<IActionResult> CreateDoctor([FromBody]DoctorModel.Request request) 
        //cuando queremos retornar mensajes de estado. Codigos de estado. (IActionResult)
        {
            //validaciones
            if (string.IsNullOrWhiteSpace(request.Name) || string.IsNullOrWhiteSpace(request.LicenseNumber))
            {
                return BadRequest("nombre y matricula son requeridos");
            }
            var speciality = _persistence.GetSpecialityById(request.SpecialityId);
            if (speciality == null)
            {
                return BadRequest("la especialidad no existe");
            }
            
            //acá hay q crear el objeto doctor con los datos que recibe.
            // a ese objeto guardarlo en persistencia en memoria.
            // en la persistencia en memoria agregar el metodo CreateDoctor
            
            var newDoctor=new Doctor(
                Guid.NewGuid(),
                request.Name,
                request.LicenseNumber,
                true,
                speciality);
            _persistence.AddDoctor(newDoctor);
            return Created();
        } 
        
        //endpoint2
        [HttpGet]
        public async Task<IActionResult> GetActiveDoctors()
        {
            return Ok();
        }
    }
}
