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




namespace AppPassword.ViewModels
{
    public class HomePageViewModel/* : INotifyPropertyChanged*/
    {
        //public event PropertyChangedEventHandler PropertyChanged;

        //protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}

        //// Change the name from Contact to Contacts
        ////public ObservableCollection<Contact> Contacts { get; set; } = new ObservableCollection<Contact>();

        //public ICommand AddCommand { get; private set; }
        //public ObservableCollection<AppPassword.Models.Contact> Contacts { get; set; } = new ObservableCollection<AppPassword.Models.Contact>();


        //// Ensure that this field is also internal to match the accessibility level of Contact_DAO
        //internal Contact_DAO _ContactDAO;

        //public HomePageViewModel()
        //{
        //    AddCommand = new Command(ExecuteAddCommand);
        //    _ContactDAO = new Contact_DAO();
        //    var credentials = _ContactDAO.GetAllContact();

        //    foreach (var credential in credentials)
        //    {
        //        Contacts.Add(credential);
        //    }

        //}

        //private void ExecuteAddCommand()
        //{
        //    // Code to execute when the AddCommand is triggered
        //    // You can implement the addition of a new record here
        //}
    }
}
