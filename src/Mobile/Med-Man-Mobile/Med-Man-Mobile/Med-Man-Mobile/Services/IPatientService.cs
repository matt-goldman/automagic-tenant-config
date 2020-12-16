using SSW.MedMan;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MedManMobile.Services
{
    public interface IPatientService
    {
        Task<List<PatientDTO>> GetAllPAtients();

        Task<PatientDTO> GetPatient(int id);

        Task<bool> UpdatePatient(PatientDTO patient);

        Task<bool> AddPatient(PatientDTO patient);
    }
}
