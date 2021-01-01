using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Med_Man_Mobile.ViewModels;
using MedManMobile.Services;
using SSW.MedMan;
using Xamarin.Forms;

namespace MedManMobile.ViewModels
{
    public class PatientsViewModel : BaseViewModel
    {
        private readonly IPatientService _patientsService;

        public ICommand RefreshCommand => new Command(async () => await ResfreshPatients());
        public bool IsRefreshing { get; set; }

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

            IsBusy = false;
        }

        private async Task ResfreshPatients()
        {
            IsRefreshing = true;
            OnPropertyChanged("IsRefreshing");

            await Initialise();

            IsRefreshing = false;
            OnPropertyChanged("IsRefreshing");
        }
    }
}