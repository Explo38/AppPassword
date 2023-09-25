using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using System.Linq;
using AppPassword.Models;
using AppPassword.ViewModels;
using AppPassword.Views;
using System.Threading.Tasks;
using AppPassword.Services;
using Contact = AppPassword.Models.Contact;
using Rg.Plugins.Popup.Services;

namespace AppPassword.ViewModels
{
    public class HomePageViewModel : INotifyPropertyChanged
    {
        public ICommand AddCommand { get; private set; }
        public ObservableCollection<Contact> Contacts { get; set; }
        // Ensure that this field is also internal to match the accessibility level of Contact_DAO
        private Contact_DAO _ContactDAO;
        public Command AjoutCommand { get; private set; }
        public HomePageViewModel()
        {
            AjoutCommand = new Command(ExecuteAjoutCommand);
            AddCommand = new Command(ExecuteAddCommand);
            _ContactDAO = new Contact_DAO();
            var credentials = _ContactDAO.GetAllContact();
            Contacts = new ObservableCollection<Contact>();
            foreach (var credential in credentials)
            {
                Contacts.Add(credential);
            }

        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private async void ExecuteAjoutCommand()
        {
            var popup = new PopupPageAjouter();
            // Affichez la popup
            await PopupNavigation.Instance.PushAsync(popup);
        }

        private void ExecuteAddCommand()
        {
            // Code to execute when the AddCommand is triggered
            // You can implement the addition of a new record here
        }
    }
}