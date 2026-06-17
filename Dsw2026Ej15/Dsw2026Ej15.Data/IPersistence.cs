using Dsw2026Ej15.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dsw2026Ej15.Data
{
    public interface IPersistence
    {
        IEnumerable<Doctor> GetDoctores();
        Doctor GetDoctorId(Guid id);
        void AddDoctor(Doctor doctor);
        IEnumerable<Speciality> GetEspecialidades();
        Speciality? GetSpecialityById(Guid id);
    }
}
