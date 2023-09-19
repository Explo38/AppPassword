using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using AppPassword.Views;
using AppPassword.Services;
using AppPassword.Models;
using Contact = AppPassword.Models.Contact;

namespace AppPassword.ViewModels
{
    public class HomePageViewModel : BaseViewModel
    {
        public ICommand AddCommand { get; private set; }
        public ObservableCollection<Contact> Contacts { get; set; }
        // Ensure that this field is also internal to match the accessibility level of Contact_DAO
        private Contact_DAO _ContactDAO;

        public HomePageViewModel()
        {
            AddCommand = new Command(ExecuteAddCommand);
            _ContactDAO = new Contact_DAO();
            var credentials = _ContactDAO.GetAllContact();
            Contacts = new ObservableCollection<Contact>();
            foreach (var credential in credentials)
            {
                Contacts.Add(credential);
            }

        }
        private void ExecuteAddCommand()
        {
            // Code to execute when the AddCommand is triggered
            // You can implement the addition of a new record here
        }
    }
}