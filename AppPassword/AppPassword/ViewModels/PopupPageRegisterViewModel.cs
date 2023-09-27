using AppPassword.Models; 
using AppPassword.Services;
using BCrypt.Net;
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
            ClosePopupCommand = new Command(ClosePopup);
            RegisterCommand = new Command( async () => await ExecuteRegister());
        }

        private void ClosePopup()
        {
            // Code pour fermer la popup
            // Par exemple : await PopupNavigation.Instance.PopAsync();
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
                First_Name = FirstName,
                Last_Name = Name, // J'ai supposé que "Name" est le nom de famille ici
                Email = Email,
                Password_Hash = hashedPassword
                // Remplissez les autres propriétés si nécessaire...
            };

            await _contactDAO.AddUser(newUser);

            // Vous pouvez ajouter une logique pour vérifier la réussite de l'ajout.
            // Après avoir ajouté avec succès, fermez la popup ou informez l'utilisateur.


            ClosePopup();
        }
    }
}
