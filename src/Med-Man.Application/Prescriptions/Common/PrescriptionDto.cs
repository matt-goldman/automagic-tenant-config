namespace MedMan.Application.Prescriptions.Common
{
    public class PrescriptionDto
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public int MedicationId { get; set; }
        public string MedicationName { get; set; }
        public int Dose { get; set; }
    }
}
