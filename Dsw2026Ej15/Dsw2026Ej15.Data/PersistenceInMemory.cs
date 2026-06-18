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

        public IEnumerable<Doctor> GetDoctores()
        {
            return _doctors;
        }

        public Doctor GetDoctorId(Guid id)
        {
            return _doctors.SingleOrDefault(d => d.Id == id);
        }

        public void AddDoctor(Doctor doctor)
        {
            _doctors.Add(doctor);
        }
        
        public IEnumerable<Speciality> GetSpecialities()
        {
            return _specialities;
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
            catch (Exception ex) { }

        }
        public Speciality? GetSpecialityById(Guid Id)
        {
            return _specialities.SingleOrDefault(e => e.Id == Id);
        }
    }
}
