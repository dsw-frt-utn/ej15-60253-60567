using Dsw2026Ej15.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Dsw2026Ej15.Data
{
    public class PersistenceEf : IPersistence
    {
        private readonly Dsw2026Ej15DbContext _context;

        public PersistenceEf(Dsw2026Ej15DbContext context)
        {
            _context = context;
        }
        public async Task AddDoctor(Doctor doctor)
        {
            _context.Add(doctor);
            await _context.SaveChangesAsync();
        }

        public Task DeactivateDoctor(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Doctor>> GetDoctores()
        {
            return await _context.Doctors.Where(d => d.IsActive).ToListAsync();
        }

        public async Task<Doctor?> GetDoctorId(Guid id)
        {
            return await _context.Doctors.FirstOrDefaultAsync(d => d.Id == id && d.IsActive);
        }

        public Task<IEnumerable<Speciality>> GetSpecialities()
        {
            throw new NotImplementedException();
        }

        public Task<Speciality?> GetSpecialityById(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
