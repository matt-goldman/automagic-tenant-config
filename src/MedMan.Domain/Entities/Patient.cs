using System;
using System.Collections.Generic;

namespace MedMan.Domain.Entities
{
    public class Patient : BaseModel
    {
        public string firstName { get; set; }
        public string familyName { get; set; }

        public string FullName
        {
            get
            {
                return firstName + " " + familyName;
            }
        }

        public DateTime DOB { get; set; }
        public List<Prescription> prescriptions { get; set; }
        public List<Administration> administrations { get; set; }
    }
}
