using Dsw2026Ej15.Api.Models;
using Dsw2026Ej15.Data;
using Dsw2026Ej15.Domain;
using Dsw2026Ej15.Domain.Exceptions;
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
        [HttpPost]
        public async Task<IActionResult> CreateDoctor([FromBody]DoctorModel.Request request) 
        {
            //valid
            if (string.IsNullOrWhiteSpace(request.Name) || string.IsNullOrWhiteSpace(request.LicenseNumber))
            {
                throw new ValidationException("nombre y matricula son requeridos");
            }
            var speciality = await _persistence.GetSpecialityById(request.SpecialityId);
            if (speciality == null)
            {
                throw new ValidationException("la especialidad no existe");
            }
            
            var newDoctor=new Doctor(
                Guid.NewGuid(),
                request.Name,
                request.LicenseNumber,
                true,
                speciality
            );
            await _persistence.AddDoctor(newDoctor);
            return Created();
        } 
        
        //endpoint2
        [HttpGet]
        public async Task<IActionResult> GetActiveDoctors()
        {
            var allDoctors = await _persistence.GetDoctores();
            var activeDoctors = allDoctors.Where(d => d.IsActive);
            return Ok(activeDoctors);
        }
        
        //endpoint3
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetDoctorId(Guid id)
        {
            var doctor=await _persistence.GetDoctorId(id);
            if(doctor == null || !doctor.IsActive)
            {
                return NotFound("doctor no encontrado o inactivo");
            }
            var data = new
            {
                Name = doctor.Name,
                LicenseNumber = doctor.LicenseNumber,
                SpecialityName = doctor.Speciality?.Name
            };
            return Ok(data);
        }
        
        //endpoint4
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteDoctorId(Guid id)
        {
            var doctorbuscardo = await _persistence.GetDoctorId(id);
            if (doctorbuscardo == null || !doctorbuscardo.IsActive)
            {
                return NotFound("Doctor no encontrado o inactivo");
            }
            await _persistence.DeactivateDoctor(id);
            return NoContent();
        }
    }
}
