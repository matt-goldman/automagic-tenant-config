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

        protected override void ReinitClient()
        {
            base.ReinitClient();
            _patientsClient = new PatientsClient(apiUri, httpClient);
        }

        public async Task<int> AddPatient(PatientDTO patient)
        {
            try
            {
                var result = await _patientsClient.PostPatientAsync(patient);
                return result.Id;
            }
            catch(ApiException apiEx)
            {
                if(apiEx.StatusCode == 401)
                {
                    bool reauth = await HandleUnauthorizedAsync();
                    if(reauth)
                    {
                        var result = await _patientsClient.PostPatientAsync(patient);
                        return result.Id;
                    }
                    else
                    {
                        Debug.WriteLine("Could not reauthenticate");
                        return 0;
                    }
                }
                else
                {
                    Debug.WriteLine("Unexpected API error");
                    Debug.WriteLine(apiEx.Message);
                    return 0;
                }
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
            try
            {
                var result = await _patientsClient.GetPatientsAsync();

                return result.ToList();
            }
            catch (ApiException apiEx)
            {
                if (apiEx.StatusCode == 401)
                {
                    bool reauth = await HandleUnauthorizedAsync();
                    if (reauth)
                    {
                        var result = await _patientsClient.GetPatientsAsync();

                        return result.ToList();
                    }
                    else
                    {
                        Debug.WriteLine("Could not reauthenticate");
                        return null;
                    }
                }
                else
                {
                    Debug.WriteLine("Unexpected API error");
                    Debug.WriteLine(apiEx.Message);
                    return null;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Could not get patients");
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<PatientDTO> GetPatient(int id)
        {
            try
            {
                var result = await _patientsClient.GetPatientAsync(id);
                return result;
            }
            catch (ApiException apiEx)
            {
                if (apiEx.StatusCode == 401)
                {
                    bool reauth = await HandleUnauthorizedAsync();
                    if (reauth)
                    {
                        var result = await _patientsClient.GetPatientAsync(id);
                        return result;
                    }
                    else
                    {
                        Debug.WriteLine("Could not reauthenticate");
                        return null;
                    }
                }
                else
                {
                    Debug.WriteLine("Unexpected API error");
                    Debug.WriteLine(apiEx.Message);
                    return null;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Could not get patient");
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<bool> UpdatePatient(PatientDTO patient)
        {
            try
            {
                await _patientsClient.PutPatientAsync(patient.Id, patient);
                return true;
            }
            catch (ApiException apiEx)
            {
                if (apiEx.StatusCode == 401)
                {
                    bool reauth = await HandleUnauthorizedAsync();
                    if (reauth)
                    {
                        await _patientsClient.PutPatientAsync(patient.Id, patient);
                        return true;
                    }
                    else
                    {
                        Debug.WriteLine("Could not reauthenticate");
                        return false;
                    }
                }
                else
                {
                    Debug.WriteLine("Unexpected API error");
                    Debug.WriteLine(apiEx.Message);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Could not update patient");
                Debug.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
