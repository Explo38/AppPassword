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
            // Tente de récupérer un utilisateur à partir de son email.
            var user = await _contactDAO.GetUserByEmail(User);

            // Si l'utilisateur est trouvé.
            if (user != null)
            {
                // Vérifie que le mot de passe fourni correspond au mot de passe haché de l'utilisateur.
                if (BCrypt.Net.BCrypt.Verify(Password, user.password_hash))
                {
                    // Navigue vers la page d'accueil si la vérification du mot de passe est réussie.
                    await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
                }
                else
                {
                    // Affiche un message d'erreur si le mot de passe est incorrect.
                    Erreur = "Mot de passe incorrect";

                    // Notifie les éventuels abonnés que la propriété 'Erreur' a changé.
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Erreur)));
                }
            }
            else
            {
                // Affiche un message d'erreur si l'utilisateur n'est pas trouvé.
                Erreur = "Utilisateur introuvable";

                // Notifie les éventuels abonnés que la propriété 'Erreur' a changé.
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Erreur)));
            }
        }



        private void RegisterUser()
        {
            // Vérifie si tous les champs requis sont remplis.
            if (string.IsNullOrWhiteSpace(Name) ||
                string.IsNullOrWhiteSpace(FirstName) ||
                string.IsNullOrWhiteSpace(Email) ||
                string.IsNullOrWhiteSpace(Password) ||
                string.IsNullOrWhiteSpace(ConfirmPassword))
            {
                // Affiche un message d'erreur si un ou plusieurs champs sont vides.
                Erreur = "Veuillez remplir tous les champs.";

                // Notifie les éventuels abonnés que la propriété 'Erreur' a changé.
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(Erreur)));
                return;
            }

            // Vérifie si le mot de passe et la confirmation du mot de passe correspondent.
            if (Password != ConfirmPassword)
            {
                // Affiche un message d'erreur si les mots de passe ne correspondent pas.
                Erreur = "Les mots de passe ne correspondent pas.";

                // Notifie les éventuels abonnés que la propriété 'Erreur' a changé.
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(Erreur)));
                return;
            }


            // Ferme la fenêtre ou la popup d'enregistrement après la vérification des informations.
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


