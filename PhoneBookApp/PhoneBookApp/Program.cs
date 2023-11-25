using PhoneBookApp.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBookApp
{
    public class Program
    {
        static Dictionary<Contact, List<Call>> phoneBook = new Dictionary<Contact, List<Call>>();
        static void Main(string[] args)
        {
            Contact contact1 = new Contact("Marin", "Cecić", "091 234 5678", Prefference.Favourite);
            Contact contact2 = new Contact("Marta", "Katić", "095 539 7809", Prefference.Normal);
            Contact contact3 = new Contact("Rina", "Miočić", "091 329 1312", Prefference.Normal);
            Contact contact4 = new Contact("Marta", "Katić", "099 530 7889", Prefference.Normal);
            Contact contact5 = new Contact("Stipe", "Bilonić", "092 234 7799", Prefference.Favourite);
            Contact contact6 = new Contact("Marta", "Katić", "091 100 4429", Prefference.Blocked);
            Contact contact7 = new Contact("Paula", "Vranjić", "091 3463 977", Prefference.Normal);

            phoneBook.Add(contact1, new List<Call>());
            phoneBook.Add(contact2, new List<Call>());
            phoneBook.Add(contact3, new List<Call>());
            phoneBook.Add(contact4, new List<Call>());
            phoneBook.Add(contact5, new List<Call>());
            phoneBook.Add(contact6, new List<Call>());
            phoneBook.Add(contact7, new List<Call>());
        }
    }
}
