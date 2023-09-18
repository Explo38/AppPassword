using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppPassword.Models;


namespace AppPassword.Services
{
    internal class Contact_DAO
    {
        public List<Contact> _ContactList { get; set; }

        public Contact_DAO()
        {
            _ContactList = new List<Contact>
            {
                new Contact
                {
                    Id = 1,
                    User = "1234",
                    Password = "1234",
                    FirstName = "John",
                    LastName = "Doe",
                    Birth = "01/01/2000",
                    Phone = "123-456-7890",
                    Email = "john.doe@example.com"
                },
                new Contact
                {
                    Id = 2,
                    User = "SecondUser",
                    Password = "SecondPass",
                    FirstName = "Jane",
                    LastName = "Smith",
                    Birth = "02/02/1995",
                    Phone = "098-765-4321",
                    Email = "jane.smith@example.com"
                },
                new Contact
                {
                    Id = 3,
                    User = "ThirdUser",
                    Password = "ThirdPass",
                    FirstName = "Robert",
                    LastName = "Johnson",
                    Birth = "03/03/1985",
                    Phone = "111-222-3333",
                    Email = "robert.johnson@example.com"
                }
            };
        }
        public IEnumerable<Contact> GetAllContact()
        {
            return _ContactList;
        }

        public bool CheckUserExists(string user)
        {
            return _ContactList.Any(contact => contact.User == user);
        }

        public string GetPassword(string user)
        {
            var contact = _ContactList.FirstOrDefault(c => c.User == user);
            return contact != null ? contact.Password : null;
        }
    }
}
