using AppPassword.Models; 
using AppPassword.Services;
using BCrypt.Net;
using Rg.Plugins.Popup.Services;
using System;
using System.Threading.Tasks; 
using System.Windows.Input;
using Xamarin.Forms;

namespace AppPassword.ViewModels
{
    public class PopupPageRegisterViewModel : BaseViewModel
    {
        private readonly Contact_DAO _contactDAO;

        public string Name { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Erreur { get; set; }

        public ICommand ClosePopupCommand { get; set; }
        public ICommand RegisterCommand { get; set; }

        public PopupPageRegisterViewModel()
        {
            _contactDAO = new Contact_DAO(); // Instanciation de Contact_DAO
            ClosePopupCommand = new Command( async () => await ClosePopup());
            RegisterCommand = new Command( async () => await ExecuteRegister());
        }

        private async Task ClosePopup()
        {
            // Code pour fermer la popup
            await PopupNavigation.Instance.PopAsync();
        }

        private async Task ExecuteRegister()
        {
            if (string.IsNullOrWhiteSpace(Name) ||
                string.IsNullOrWhiteSpace(FirstName) ||
                string.IsNullOrWhiteSpace(Email) ||
                string.IsNullOrWhiteSpace(Password) ||
                string.IsNullOrWhiteSpace(ConfirmPassword))
            {
                Erreur = "Veuillez remplir tous les champs.";
                OnPropertyChanged(nameof(Erreur));
                return;
            }

            if (Password != ConfirmPassword)
            {
                Erreur = "Les mots de passe ne correspondent pas.";
                OnPropertyChanged(nameof(Erreur));
                return;
            }

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(Password);

            Contact newUser = new Contact
            {
                first_name = FirstName,
                last_name = Name, // J'ai supposé que "Name" est le nom de famille ici
                email = Email,
                password_hash = hashedPassword
                // Remplissez les autres propriétés si nécessaire...
            };

            await _contactDAO.AddUser(newUser);

            // Vous pouvez ajouter une logique pour vérifier la réussite de l'ajout.
            // Après avoir ajouté avec succès, fermez la popup ou informez l'utilisateur.


            ClosePopup();
        }
    }
}

