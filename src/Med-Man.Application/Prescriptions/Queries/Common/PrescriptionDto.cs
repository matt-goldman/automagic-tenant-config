using MedMan.Application.Common.Mappings;
using MedMan.Application.Medications.Queries.Common;
using MedMan.Application.Patients.Queries.Common;
using MedMan.Domain.Entities;

namespace MedMan.Application.Prescriptions.Queries.Common
{
    public class PrescriptionDto : IMapFrom<Prescription>
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public int MedicationId { get; set; }
        public string MedicationName { get; set; }
        public int Dose { get; set; }
        public MedicationDto Medication { get; set; }
        public PatientDto Patient { get; set; }
    }
}
