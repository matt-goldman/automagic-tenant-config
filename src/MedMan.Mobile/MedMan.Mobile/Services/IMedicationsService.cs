using SSW.MedMan;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MedMan.Mobile.Services
{
    public interface IMedicationsService
    {
        Task<MedicationDTO> GetMedication(int id);
        Task<List<MedicationDTO>> GetMedications();
        Task<int> AddMedication(MedicationDTO medication);
        Task<bool> UpdateMedication(MedicationDTO medication);
    }
}
