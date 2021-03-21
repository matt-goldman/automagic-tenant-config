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
            try
            {
                ViewModel = Resolver.Resolve<PatientsViewModel>();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            

            BindingContext = ViewModel;

            InitializeComponent();
        }
    }
}