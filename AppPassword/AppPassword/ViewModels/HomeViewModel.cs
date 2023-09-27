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
using System.Globalization;
using AppPassword.ViewModels;

namespace AppPassword.ViewModels
{
    public class HomePageViewModel : INotifyPropertyChanged
    {
        public ICommand AddCommand { get; private set; }
        public ObservableCollection<PasswordEntry> PasswordEntries { get; set; }
        private Password_DAO _PasswordDAO;
        public Command AjoutCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public ICommand ConfirmDeleteCommand { get; private set; }
        public ICommand CancelDeleteCommand { get; private set; }

        public HomePageViewModel()
        {
            AjoutCommand = new Command(ExecuteAjoutCommand);
            AddCommand = new Command(ExecuteAddCommand);
            _PasswordDAO = new Password_DAO();
            DeleteCommand = new Command<PasswordEntry>(ExecuteDeleteCommand);
            ConfirmDeleteCommand = new Command<PasswordEntry>(ExecuteConfirmDeleteCommand);
            CancelDeleteCommand = new Command<PasswordEntry>(ExecuteCancelDeleteCommand);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private async void ExecuteAjoutCommand()
        {
            var popup = new PopupPageAjouter();
            await PopupNavigation.Instance.PushAsync(popup);
        }

        private void ExecuteAddCommand()
        {
            // Code pour ajouter une nouvelle entrée de mot de passe
        }

        private void ExecuteDeleteCommand(PasswordEntry passwordEntry)
        {
            passwordEntry.IsDeleteMode = true;
        }

        private void ExecuteConfirmDeleteCommand(PasswordEntry passwordEntry)
        {
            PasswordEntries.Remove(passwordEntry);
            // Code pour supprimer de la base de données si nécessaire
        }

        private void ExecuteCancelDeleteCommand(PasswordEntry passwordEntry)
        {
            passwordEntry.IsDeleteMode = false;
        }

        public class InverseBoolConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                if (value is bool boolValue)
                    return !boolValue;
                return value;
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                if (value is bool boolValue)
                    return !boolValue;
                return value;
            }
        }
    }
}