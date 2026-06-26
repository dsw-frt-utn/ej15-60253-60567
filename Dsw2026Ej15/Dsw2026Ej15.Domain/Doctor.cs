using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Dsw2026Ej15.Domain
{
   
    public class Doctor : BaseEntity
    {
        public string Name { get; init; }
        public string LicenseNumber { get; init; }
        public bool IsActive { get; private set; }
        public Guid? SpecialityId { get; set; }
        public Speciality Speciality { get; private set; }

        private Doctor(){}
        public Doctor(Guid id, string name, string licenseNumber, bool isActive, Speciality speciality) : base(id)
        {
            Name = name;
            LicenseNumber = licenseNumber;
            IsActive = isActive;
            Speciality = speciality;
        }

        //public void Deactivate()
        //{
         //   IsActive = false;
        //}
    }
}