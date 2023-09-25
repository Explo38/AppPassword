using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace AppPassword.ViewModels
{
	public class PopupPageRegisterViewModel : BaseViewModel
    {
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        //public ICommand ClosePopupCommand { get; set; }
        //public ICommand RegisterCommand { get; set; }

        public PopupPageRegisterViewModel()
        {
            //ClosePopupCommand = new Command(ClosePopup);
            //RegisterCommand = new Command(ExecuteRegister);
        }

        private void ClosePopup()
        {
            // Code pour fermer la popup
        }

        private void ExecuteRegister()
        {
            // Code pour enregistrer le nouveau contact
            // Vous devrez ajouter la logique pour créer un nouveau contact ici
            // Assurez-vous de valider les champs et de gérer les erreurs si nécessaire
        }
    }
}