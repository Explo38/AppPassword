using AppPassword.Models;
using AppPassword.Services;
using Rg.Plugins.Popup.Services;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppPassword.ViewModels
{
    public class PopupPageAjouterViewModel : BaseViewModel
    {
        private readonly Password_DAO _passwordDAO;

        public string Name { get; set; }              // pour le champ "Nom"
        public string URL { get; set; }               // pour le champ "URL"
        public string Password { get; set; }          // pour le champ "Mot de passe"
        public string ConfirmPassword { get; set; }   // pour le champ "Confirmation de mot de passe"
        public string Description { get; set; }       // pour le champ "Description"
        public string Erreur { get; set; }

        public ICommand ClosePopupCommand { get; set; }
        public ICommand AddPasswordEntryCommand { get; set; }

        public PopupPageAjouterViewModel()
        {
            _passwordDAO = new Password_DAO();
            ClosePopupCommand = new Command(async () => await ClosePopup());
            AddPasswordEntryCommand = new Command(async () => await ExecuteAddPasswordEntry());
        }

        private async Task ClosePopup()
        {
            await PopupNavigation.Instance.PopAsync();
        }

        private async Task ExecuteAddPasswordEntry()
        {
            if (string.IsNullOrWhiteSpace(Name) ||
                string.IsNullOrWhiteSpace(Password) ||
                string.IsNullOrWhiteSpace(ConfirmPassword))
            {
                Erreur = "Veuillez remplir les champs obligatoires.";
                OnPropertyChanged(nameof(Erreur));
                return;
            }

            if (Password != ConfirmPassword)
            {
                Erreur = "Les mots de passe ne correspondent pas.";
                OnPropertyChanged(nameof(Erreur));
                return;
            }

            PasswordEntry newPasswordEntry = new PasswordEntry
            {
                site_web = Name,
                url_site_web = URL ?? string.Empty,           // Si URL est null, assignez une chaîne vide
                PasswordEncrypted = Password,               // Ici, je suppose que vous allez chiffrer le mot de passe plus tard.
                description = Description ?? string.Empty  // Si Description est null, assignez une chaîne vide
                // Remplissez les autres propriétés si nécessaire...
            };

            await _passwordDAO.AddPasswordEntry(newPasswordEntry);

            // Envoyer un message pour informer qu'une nouvelle entrée de mot de passe a été ajoutée
            MessagingCenter.Send(this, "Nouveau site ajouter", newPasswordEntry);

            ClosePopup();
        }
    }
}

