using SSW.MedMan;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MedMan.Mobile.Services
{
    public interface IPrescriptionsService
    {
        Task<int> AddPrescription(PrescriptionDTO prescription);
        Task<bool> UpdatePrescription(PrescriptionDTO prescription);
        Task<List<PrescriptionDTO>> GetPrescriptions();
        Task<PrescriptionDTO> GetPrescription(int id);
        Task<bool> DeletePrescription(int id);
    }
}
