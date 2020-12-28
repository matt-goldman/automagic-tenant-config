using System.Collections.Generic;
using System.Threading.Tasks;
using Med_Man_Mobile.ViewModels;
using MedManMobile.Services;
using SSW.MedMan;

namespace MedManMobile.ViewModels
{
    public class PatientsViewModel : BaseViewModel
    {
        private readonly IPatientService _patientsService;
        public List<PatientDTO> Patients { get; set; }


        public PatientsViewModel(IPatientService patientsService)
        {
            _patientsService = patientsService;
            _ = Initialise();
        }

        private async Task Initialise()
        {
            IsBusy = true;
            
            Patients = await _patientsService.GetAllPAtients();
            OnPropertyChanged("Patients");
        }
    }
}