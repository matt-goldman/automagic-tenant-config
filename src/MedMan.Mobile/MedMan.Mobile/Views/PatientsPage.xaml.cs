using MedMan.Mobile.ViewModels;
using System;
using Xamarin.Forms;


namespace MedMan.Mobile.Views
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