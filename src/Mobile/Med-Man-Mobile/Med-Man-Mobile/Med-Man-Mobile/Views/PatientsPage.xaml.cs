using MedManMobile.ViewModels;
using System;
using Xamarin.Forms;


namespace MedManMobile.Views
{
    public partial class PatientsPage : ContentPage
    {
        private PatientsViewModel ViewModel { get; set; }

        public PatientsPage()
        {
            ViewModel = Resolver.Resolve<PatientsViewModel>();

            BindingContext = ViewModel;

            InitializeComponent();
        }
    }
}