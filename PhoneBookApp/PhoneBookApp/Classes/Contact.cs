using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhoneBookApp.Enums;

namespace PhoneBookApp
{
    public class Contact
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }

        public Prefference Prefference { get; set; }

        public Contact(string firstName, string lastName, string phoneNumber, Prefference prefference)
        {
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            Prefference = prefference;
        }
    }
}
