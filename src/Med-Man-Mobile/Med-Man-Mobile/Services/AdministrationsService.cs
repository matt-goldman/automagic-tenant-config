using SSW.MedMan;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MedManMobile.Services
{
    public class AdministrationsService : BaseService, IAdministrationsService
    {
        private AdministrationsClient _administrationsClient;

        public AdministrationsService()
        {
            _administrationsClient = new AdministrationsClient(apiUri, httpClient);
        }

        public async Task<bool> DeleteAdministration(int id)
        {
            try
            {
                await _administrationsClient.DeleteAdministrationsAsync(id);
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Couldn't delete administration");
                Debug.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<AdministrationDTO> GetAdministrationDTO(int id)
        {
            try
            {
                var result = await _administrationsClient.GetAdministrationsAsync(id);
                return result;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Couldn't get administration");
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<List<AdministrationDTO>> GetAdministrations()
        {
            try
            {
                var result = await _administrationsClient.GetAdministrationsAllAsync();
                return result.ToList();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Couldn't get administrations");
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<int> PostAdministration(AdministrationDTO administration)
        {
            try
            {
                var result = await _administrationsClient.PostAdministrationsAsync(administration);
                return result.Id;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Couldn't create administration");
                Debug.WriteLine(ex.Message);
                return 0;
            }
        }

        public async Task<bool> UpdateAdministration(AdministrationDTO administration)
        {
            try
            {
                await _administrationsClient.PutAdministrationsAsync(administration.Id, administration);
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Couldn't update administration");
                Debug.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
