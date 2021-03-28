using MedMan.Mobile.ViewModels;
using Xamarin.Forms;


namespace MedMan.Mobile.Views
{
    public partial class ConfigPage : ContentPage
    {
        private ConfigViewModel ViewModel { get; set; }

        public ConfigPage()
        {
            InitializeComponent();
            ViewModel = new ConfigViewModel();
            ViewModel.Navigation = Navigation;
            BindingContext = ViewModel;
        }
    }
}