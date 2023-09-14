using AppPassword.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace AppPassword.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public Command LoginCommand { get; }
        public string User { get; set; }
        public string Password { get; set; }
        public string Erreur { get; set; }
        public LoginViewModel()
        {
            LoginCommand = new Command(OnLoginClicked);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private async void OnLoginClicked(object obj)
        {
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            if (User == "1234" & Password == "1234")
            {
                await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
            }
            else
            {
                Erreur = "Erreur";
                PropertyChanged(this, new PropertyChangedEventArgs(Erreur));
            }
        }
    }
}
