using AppPassword.Services;
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
        private readonly Contact_DAO _contactDAO;

        public LoginViewModel()
        {
            _contactDAO = new Contact_DAO(); // Instanciez Contact_DAO
            LoginCommand = new Command(OnLoginClicked);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private async void OnLoginClicked(object obj)
        {
            // Utilisez Contact_DAO pour vérifier le nom d'utilisateur
            bool isValidUser = _contactDAO.CheckUserExists(User);

            if (isValidUser)
            {
                // Récupérez le mot de passe depuis Contact_DAO
                string correctPassword = _contactDAO.GetPassword(User);

                if (Password == correctPassword)
                {
                    await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
                }
                else
                {
                    Erreur = "Mot de passe incorrect";
                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(Erreur)));
                }
            }
            else
            {
                Erreur = "Utilisateur introuvable";
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(Erreur)));
            }
        }
    }
}

