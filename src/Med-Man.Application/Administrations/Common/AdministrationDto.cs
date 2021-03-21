using System;

namespace MedMan.Application.Administrations.Common
{
    public class AdministrationDto
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int MedicationId { get; set; }
        public int Dose { get; set; }
        public DateTime TimeGiven { get; set; }
    }
}
