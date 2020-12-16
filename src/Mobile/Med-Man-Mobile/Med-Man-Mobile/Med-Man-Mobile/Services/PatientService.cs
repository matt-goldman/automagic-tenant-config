using SSW.MedMan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedManMobile.Services
{
    public class PatientService : BaseService, IPatientService
    {
        private PatientsClient _patientsClient;

        public PatientService()
        {

        }

        public async Task<bool> AddPatient(PatientDTO patient)
        {
            throw new NotImplementedException();
        }

        public async Task<List<PatientDTO>> GetAllPAtients()
        {
            throw new NotImplementedException();
        }

        public async Task<PatientDTO> GetPatient(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdatePatient(PatientDTO patient)
        {
            throw new NotImplementedException();
        }
    }
}
