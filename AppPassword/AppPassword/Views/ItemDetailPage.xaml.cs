using AppPassword.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace AppPassword.Views
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