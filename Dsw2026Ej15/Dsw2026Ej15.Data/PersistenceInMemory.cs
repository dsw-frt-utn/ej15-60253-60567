
using System.Text.Json;
using Dsw2026Ej15.Domain;


namespace Dsw2026Ej15.Data
{
    public class PersistenceInMemory : IPersistence
    {
        private static readonly List<Doctor> _doctors = new List<Doctor>();
        private static List<Speciality> _specialities = new List<Speciality>();

        public PersistenceInMemory()
        {
            LoadSpecialities();
        }

        public Task<IEnumerable<Doctor>> GetDoctores()
        {
            return Task.FromResult<IEnumerable<Doctor>>(_doctors);
        }

        public Task<Doctor?> GetDoctorId(Guid id)
        {
            var doctor = _doctors.SingleOrDefault(d => d.Id == id);
            return Task.FromResult(doctor);
        }

        public Task AddDoctor(Doctor doctor)
        {
            _doctors.Add(doctor);
           
            return Task.CompletedTask;
        }

        public Task<IEnumerable<Speciality>> GetSpecialities()
        {
            return Task.FromResult<IEnumerable<Speciality>>(_specialities);
        }

        public Task DeactivateDoctor(Guid id)
        {
            var doctorActual = _doctors.SingleOrDefault(d => d.Id == id);

            if (doctorActual != null)
            {
                var doctorInactivo = new Doctor(
                    doctorActual.Id,
                    doctorActual.Name,
                    doctorActual.LicenseNumber,
                    false, // lo actualiza a inactivo
                    doctorActual.Speciality
                );

                _doctors.Remove(doctorActual);
                _doctors.Add(doctorInactivo);
            }

            return Task.CompletedTask;
        }

        private void LoadSpecialities()
        {
            try
            {
                string jsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                    "Sources", "specialities.json");
                var json = File.ReadAllText(jsonPath);

                var specialitites = JsonSerializer.Deserialize<List<Speciality>>
                    (json, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }) ?? [];


                _specialities = [.. specialitites.Select(s => new Speciality(s.Id, s.Name, s.Description))];
            }
            catch (Exception)
            {
               
            }
        }

        public Task<Speciality?> GetSpecialityById(Guid Id)
        {
            var speciality = _specialities.SingleOrDefault(e => e.Id == Id);
            return Task.FromResult(speciality);
        }
    }
}
