using System;
using System.Collections.Generic;
using System.Text;

namespace AppPassword.Models
{
    public class Contact
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string User { get; set; }
        public string Birth { get; set; }
    }

    public class PasswordEntry
    {
        public int Id { get; set; }
        public string SiteWeb { get; set; }
        public string Description { get; set; }

        public string MdpHash { get; set; }

        public int UserId { get; set; }

    }
}
