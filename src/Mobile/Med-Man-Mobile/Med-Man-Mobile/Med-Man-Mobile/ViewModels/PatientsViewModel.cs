using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Med_Man_Mobile.ViewModels;
using Med_Man_Mobile;
using MedManMobile.Services;
using SSW.MedMan;
using Xamarin.Forms;
using MedManMobile.Views;

namespace MedManMobile.ViewModels
{
    public class PatientsViewModel : BaseViewModel
    {
        private readonly IPatientService _patientsService;
        public INavigation Navigation { get; set; }

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
            if (!App.Constants.SecretsInitialised)
            {
                await Navigation.PushModalAsync(new ConfigPage());
                return;
            }
            else if (!App.IsLoggedIn)
            {
                return;
            }

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