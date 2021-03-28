using SSW.MedMan;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MedMan.Mobile.Services
{
    public interface IAdministrationsService
    {
        Task<int> PostAdministration(AdministrationDTO administration);
        Task<bool> UpdateAdministration(AdministrationDTO administration);
        Task<AdministrationDTO> GetAdministrationDTO(int id);
        Task<List<AdministrationDTO>> GetAdministrations();
        Task<bool> DeleteAdministration(int id);
    }
}
