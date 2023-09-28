using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using AppPassword.ViewModels;

namespace AppPassword.Models
{
    public class PasswordEntry : INotifyPropertyChanged
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
        public int UserId { get; set; }
        public string site_web { get; set; }
        public string description { get; set; }
        public string PasswordEncrypted { get; set; }
        public string EncryptionKey { get; set; }
        public string EncryptionIV { get; set; }
        public string url_site_web { get; set; }

        // Frame de couverture pour la confirmation
        public bool IsCoverVisible => _isDeleteMode;

        // Implémentation de INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
