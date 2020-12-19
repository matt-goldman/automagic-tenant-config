using SSW.MedMan;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MedManMobile.Services
{
    public class PatientService : BaseService, IPatientService
    {
        private PatientsClient _patientsClient;

        public PatientService()
        {
            _patientsClient = new PatientsClient(apiUri, httpClient);
        }

        public async Task<int> AddPatient(PatientDTO patient)
        {
            try
            {
                var result = await _patientsClient.PostPatientAsync(patient);
                return result.Id;
            }
            catch(Exception ex)
            {
                Debug.WriteLine("Could not add patient");
                Debug.WriteLine(ex.Message);
                return 0;
            }
        }

        public async Task<List<PatientDTO>> GetAllPAtients()
        {
            var result = await _patientsClient.GetPatientsAsync();

            return result.ToList();
        }

        public async Task<PatientDTO> GetPatient(int id)
        {
            var result = await _patientsClient.GetPatientAsync(id);
            return result;
        }

        public async Task<bool> UpdatePatient(PatientDTO patient)
        {
            try
            {
                var result = await _patientsClient.PutPatientAsync(patient.Id, patient);
                return true;
            }
            catch(Exception ex)
            {
                Debug.WriteLine("Could not update patient");
                Debug.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
