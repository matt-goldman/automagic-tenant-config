using Med_Man_Mobile.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace Med_Man_Mobile.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}