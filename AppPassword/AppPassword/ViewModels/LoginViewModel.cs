using AppPassword.Services;
using AppPassword.Views;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppPassword.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public Command LoginCommand { get; }
        public string User { get; set; }
        public string Password { get; set; }
        public string Erreur { get; set; }
        private readonly Contact_DAO _contactDAO;

        public ICommand ClosePopupCommand { get; }
        public ICommand RegisterCommand { get; }
        public ICommand ShowRegisterPageCommand { get; private set; }




        // Propriétés pour la popup
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string ConfirmPassword { get; set; }




        public LoginViewModel()
        {
            _contactDAO = new Contact_DAO(); // Instanciez Contact_DAO
            LoginCommand = new Command(OnLoginClicked);

            // ... Initialisations existantes ...
            ClosePopupCommand = new Command(CloseRegisterPopup);
            RegisterCommand = new Command(RegisterUser);
            ShowRegisterPageCommand = new Command(ShowRegisterPage);



        }

        public event PropertyChangedEventHandler PropertyChanged;



        private async void OnLoginClicked(object obj)
        {
            var user = await _contactDAO.GetUserByEmail(User);

            if (user != null)
            {
                if (BCrypt.Net.BCrypt.Verify(Password, user.password_hash))
                {
                    await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
                }
                else
                {
                    Erreur = "Mot de passe incorrect";
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Erreur)));
                }
            }
            else
            {
                Erreur = "Utilisateur introuvable";
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Erreur)));
            }
        }


        private void RegisterUser()
        {
            if (string.IsNullOrWhiteSpace(Name) ||
                string.IsNullOrWhiteSpace(FirstName) ||
                string.IsNullOrWhiteSpace(Email) ||
                string.IsNullOrWhiteSpace(Password) ||
                string.IsNullOrWhiteSpace(ConfirmPassword))
            {
                Erreur = "Veuillez remplir tous les champs.";
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(Erreur)));
                return;
            }

            if (Password != ConfirmPassword)
            {
                Erreur = "Les mots de passe ne correspondent pas.";
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(Erreur)));
                return;
            }

            // À l'avenir: Ajoutez ici le code pour sauvegarder l'utilisateur dans la base de données.

            // Fermer la popup après l'enregistrement
            CloseRegisterPopup();
        }

        private async void ShowRegisterPage()
        {
            var popupPage = new PopupPageRegister(); 
            await PopupNavigation.Instance.PushAsync(popupPage);
        }


        private async void CloseRegisterPopup()
        {
            await PopupNavigation.Instance.PopAsync();
        }


    }
}


