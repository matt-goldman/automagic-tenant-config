namespace MedMan.Domain.Entities
{
    public class Prescription : BaseModel
    {
        public int patientId { get; set; }
        public Patient patient { get; set; }
        public int medicationId { get; set; }
        public Medication medication { get; set; }
        public int dose { get; set; }
    }
}
