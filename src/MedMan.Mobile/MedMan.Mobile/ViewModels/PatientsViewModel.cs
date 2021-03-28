using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using MedMan.Mobile.ViewModels;
using MedMan.Mobile;
using MedMan.Mobile.Services;
using SSW.MedMan;
using Xamarin.Forms;

namespace MedMan.Mobile.ViewModels
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
            if (!App.IsLoggedIn)
                return;

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