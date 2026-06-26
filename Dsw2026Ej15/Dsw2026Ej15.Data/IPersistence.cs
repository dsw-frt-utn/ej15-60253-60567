using Dsw2026Ej15.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dsw2026Ej15.Data
{
    public interface IPersistence
    {
        Task<IEnumerable<Doctor>> GetDoctores();
        Task<Doctor?> GetDoctorId(Guid id);
        //void AddDoctor(Doctor doctor);
        Task AddDoctor (Doctor doctor);
        Task<IEnumerable<Speciality>> GetSpecialities();
        Task<Speciality?> GetSpecialityById(Guid id);
        //void DeactivateDoctor(Guid id);
        Task DeactivateDoctor(Guid id);
    }
}
