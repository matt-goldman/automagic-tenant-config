using MedManMobile.ViewModels;
using Xamarin.Forms;


namespace MedManMobile.Views
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

        private async void ZXingScannerView_OnScanResult(ZXing.Result result)
        {
            await ViewModel.SaveScannedConfig(result.Text);
        }
    }
}