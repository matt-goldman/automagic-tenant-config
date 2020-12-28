using SSW.MedMan;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MedManMobile.Services
{
    public class MedsService : BaseService, IMedicationsService
    {
        private MedicationsClient _medicationsClient;

        public MedsService()
        {
            _medicationsClient = new MedicationsClient(apiUri, httpClient);
        }

        public async Task<int> AddMedication(MedicationDTO medication)
        {
            try
            {
                var result = await _medicationsClient.PostMedicationAsync(medication);
                return result.Id;
            }
            catch(Exception ex)
            {
                Debug.WriteLine("Couldn't add medication");
                Debug.WriteLine(ex.Message);
                return 0;
            }
        }

        public async Task<MedicationDTO> GetMedication(int id)
        {
            try
            {
                var result = await _medicationsClient.GetMedicationAsync(id);
                return result;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Couldn't get medication");
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<List<MedicationDTO>> GetMedications()
        {
            try
            {
                var result = await _medicationsClient.GetMedicationsAsync();
                return result.ToList();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Couldn't get medications");
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<bool> UpdateMedication(MedicationDTO medication)
        {
            try
            {
                await _medicationsClient.PutMedicationAsync(medication.Id, medication);
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Couldn't update medication");
                Debug.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
