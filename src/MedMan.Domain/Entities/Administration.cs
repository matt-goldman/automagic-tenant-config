using System;

namespace MedMan.Domain.Entities
{
    public class Administration : BaseModel
    {
        public int patientId { get; set; }
        public Patient patient { get; set; }
        public int medicationId { get; set; }
        public Medication medication { get; set; }
        public int dose { get; set; }
        public DateTime timeGiven { get; set; }
    }
}
