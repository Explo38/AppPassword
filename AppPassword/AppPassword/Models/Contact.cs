using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using AppPassword.ViewModels;

namespace AppPassword.Models
{
    public class Contact : INotifyPropertyChanged
    {
        private bool _isDeleteMode;

        public bool IsDeleteMode
        {
            get => _isDeleteMode;
            set
            {
                _isDeleteMode = value;
                OnPropertyChanged();
            }
        }

        public int Id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string password_hash { get; set; }
        public string User { get; set; }
        public string birth { get; set; }

        // Implémentez l'événement PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        // Implémentez la méthode pour déclencher l'événement
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Frame de couverture pour la confirmation
        public bool IsCoverVisible => _isDeleteMode;
    }


}