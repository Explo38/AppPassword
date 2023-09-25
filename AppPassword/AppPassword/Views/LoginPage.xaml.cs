using AppPassword.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Rg.Plugins.Popup.Services;

namespace AppPassword.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            this.BindingContext = new LoginViewModel();


            MessagingCenter.Subscribe<LoginViewModel>(this, "ShowRegisterPopup", async (sender) =>
            {
                var popupPage = new PopupPageRegister();
                await PopupNavigation.Instance.PushAsync(popupPage);
            });

            MessagingCenter.Subscribe<LoginViewModel>(this, "CloseRegisterPopup", async (sender) =>
            {
                await PopupNavigation.Instance.PopAsync();
            });


        }

    }
    
}