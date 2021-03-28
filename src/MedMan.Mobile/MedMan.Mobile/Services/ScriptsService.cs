using SSW.MedMan;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedMan.Mobile.Services
{
    public class ScriptsService : BaseService, IPrescriptionsService
    {
        private PrescriptionsClient _prescriptionsClient;

        public ScriptsService()
        {
            _prescriptionsClient = new PrescriptionsClient(apiUri, httpClient);
        }

        public async Task<int> AddPrescription(PrescriptionDTO prescription)
        {
            try
            {
                var result = await _prescriptionsClient.PostPrescriptionAsync(prescription);
                return result.Id;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Couldn't add prescription");
                Debug.WriteLine(ex.Message);
                return 0;
            }
        }

        public async Task<bool> DeletePrescription(int id)
        {
            try
            {
                await _prescriptionsClient.DeletePrescriptionAsync(id);
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Couldn't delete prescription");
                Debug.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<PrescriptionDTO> GetPrescription(int id)
        {
            try
            {
                var result = await _prescriptionsClient.GetPrescriptionAsync(id);
                return result;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Couldn't get prescription");
                Debug.WriteLine(ex.Message);
                return null;
            }

        }

        public async Task<List<PrescriptionDTO>> GetPrescriptions()
        {
            try
            {
                var result = await _prescriptionsClient.GetPrescriptionsAsync();
                return result.ToList();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Couldn't get prescriptions");
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<bool> UpdatePrescription(PrescriptionDTO prescription)
        {
            try
            {
                await _prescriptionsClient.PutPrescriptionAsync(prescription.Id, prescription);
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Couldn't update prescription");
                Debug.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
