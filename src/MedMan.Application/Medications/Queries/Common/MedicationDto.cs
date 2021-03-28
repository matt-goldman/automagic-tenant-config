using MedMan.Application.Common.Mappings;
using MedMan.Domain.Entities;

namespace MedMan.Application.Medications.Queries.Common
{
    public class MedicationDto : IMapFrom<Medication>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
