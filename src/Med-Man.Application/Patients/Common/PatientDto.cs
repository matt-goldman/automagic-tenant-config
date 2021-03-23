using System;

namespace MedMan.Application.Patients.Common
{
    public class PatientDto
    {
        public int Id { get; set; }
        public string GivenName { get; set; }
        public string FamilyName { get; set; }
        public string FullName => string.Format("{0} {1}", GivenName, FamilyName);
        public DateTime DOB { get; set; }
    }
}
