using PhoneBookApp.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.ComponentModel.Design;

namespace PhoneBookApp
{
    public class Program
    {
        static Dictionary<Contact, List<Call>> phoneBook = new Dictionary<Contact, List<Call>>();
        static void Main(string[] args)
        {
            Contact contact1 = new Contact("Marin", "Cecić", "091 234 5678", Prefference.Favourite);
            Contact contact2 = new Contact("Marta", "Batinić", "095 539 7809", Prefference.Normal);
            Contact contact3 = new Contact("Rina", "Miočić", "091 329 1312", Prefference.Normal);
            Contact contact4 = new Contact("Mladen", "Katić", "099 530 7889", Prefference.Normal);
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

            

            while (true)
            {
                Console.Clear();
                Console.WriteLine("----MENU----");
                Console.WriteLine("1 - Ispis svih kontakata");
                Console.WriteLine("2 - Dodavanje novih kontakata u imenik");
                Console.WriteLine("3 - Brisanje kontakata iz imenika");
                Console.WriteLine("4 - Uređivanje preference kontakta");
                Console.WriteLine("5 - Upravljanje kontaktom");
                Console.WriteLine("6 - Ispis svih poziva");
                Console.WriteLine("7 - Izlaz iz aplikacije");

                string option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        Console.Clear();
                        PrintAllContacts();
                        break;
                    case "2":
                        Console.Clear();
                        AddNewContacts();
                        break;
                    case "3":
                        Console.Clear();
                        DeleteContact();
                        break;
                    case "4":
                        Console.Clear();
                        EditPrefferences();
                        break;
                    case "5":
                        Console.Clear();
                        SubMenu();                
                        break;
                    case "6":
                        Console.Clear();
                        PrintAllCalls();
                        break;
                    case "7":
                        Console.Clear();
                        Console.WriteLine("Izlaz iz aplikacije...");
                        Thread.Sleep(1000);
                        return;
                    default:
                        Console.Clear();
                        Console.WriteLine("Pogrešan unos. Upišite brojeve 1-7.");
                        break;
                }
            }
        }

        static void SubMenu()
        {
            while (true)
            {
                Console.WriteLine("----SUBMENU----");
                Console.WriteLine("1 - Ispis svih poziva sa određenim kontaktom, vremenski poredan");
                Console.WriteLine("2 - Kreiranje novog poziva");
                Console.WriteLine("3 - Povratak na glavni izbornik");

                string option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        Console.Clear();
                        PrintAllContactCalls();
                        break;
                    case "2":
                        Console.Clear();
                        MakeNewCall();
                        break;
                    case "3":
                        Console.Clear();
                        return;
                    default:
                        Console.WriteLine("Pogrešan unos. Upišite brojeve 1-3.");
                        break;
                }
            }
        }
        static void PrintAllContacts()
        {
            Console.WriteLine("----SVI KONTAKTI----");
            
            for(int i = 0; i < phoneBook.Count(); i++)
            {
                Contact contact = phoneBook.Keys.ElementAt(i);
                Console.WriteLine($"{i+1}. {contact.FirstName} {contact.LastName} - {contact.PhoneNumber} - {contact.Prefference}");
            }
             
            Console.WriteLine();
            Console.WriteLine("Pritisnite bilo koju tipku za povratak na glavni meni...");
            Console.ReadKey();
        }

        static void AddNewContacts()
        {
            string firstName = "";
            string lastName = "";
            string phoneNumber = "";
            Prefference preff = new Prefference();

            Console.WriteLine("----UNOS NOVOG KONTAKTA----");

            while (true)
            {
                Console.Write("Unesite ime novog kontakta: ");
                firstName = Console.ReadLine();

                if (firstName == "")
                {
                    Console.WriteLine("Ime ne smije biti prazno.");
                    continue;
                }

                Console.Write("Unesite prezime novog kontakta: ");
                lastName = Console.ReadLine(); // prezime moze biti prazno

                bool contactExists = phoneBook.Keys.Any(k => k.FirstName.Equals(firstName, StringComparison.OrdinalIgnoreCase) && k.LastName.Equals(lastName, StringComparison.OrdinalIgnoreCase));


                if (contactExists)
                {
                    Console.WriteLine("Kontakt s istim imenom i prezimenom već postoji.");
                    continue;
                }

                break;
            }

            while (true)
            {
                Console.WriteLine("Unesite broj mobitela: ");
                phoneNumber = Console.ReadLine();

                if (phoneBook.Keys.Any(contact => contact.PhoneNumber.Equals(phoneNumber, StringComparison.OrdinalIgnoreCase)))
                {
                    Console.WriteLine("Kontakt s istim brojem mobitela već postoji.");
                    continue;
                }

                if (phoneNumber == "")
                {
                    Console.WriteLine("Broj mobitela ne smije biti prazan.");
                    continue;
                }

                break;
            }

            while (true)
            {
                Console.WriteLine("Odaberite broj za preferencu kontakta: ");
                Console.WriteLine("1. Favorit");
                Console.WriteLine("2. Normalan");
                Console.WriteLine("3. Blokiran");

                string option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        preff = Prefference.Favourite;
                        break;
                    case "2":
                        preff = Prefference.Normal;
                        break;
                    case "3":
                        preff = Prefference.Blocked;
                        break;
                    default:
                        Console.WriteLine("Pogrešan unos za preferencu. Upišite brojeve 1-3.");
                        continue;
                }
                break;
            }
            while (true)
            {
                Console.WriteLine($"Potvrđujete li unos kontakta {firstName} {lastName} (da/ne)?");
                string conf = Console.ReadLine();

                if (conf.ToLower() == "da")
                {
                    Contact newContact = new Contact(firstName, lastName, phoneNumber, preff);
                    phoneBook.Add(newContact, new List<Call>());

                    Console.WriteLine($"Novi kontakt {firstName} {lastName} dodan u imenik.");
                }
                else if (conf.ToLower() == "ne")
                {
                    return;
                }
                else
                {
                    Console.WriteLine("Pogrešan unos.");
                    continue;
                }
                break;
            }
            Console.WriteLine();
            Console.WriteLine("Pritisnite bilo koju tipku za povratak na glavni meni: ");
            Console.ReadKey();
        }

        static void DeleteContact()
        {
            Console.WriteLine("----BRISANJE KONTAKTA----");

            // prikaz kontakata da korisnik moze vidjeti koji obrisati
            for (int i = 0; i < phoneBook.Count(); i++)
            {
                Contact contact = phoneBook.Keys.ElementAt(i);
                Console.WriteLine($"{i + 1}. {contact.FirstName} {contact.LastName} - {contact.PhoneNumber} - {contact.Prefference}");
            }

            Console.WriteLine();

            Console.Write("Unesite ime kontakta koji se briše: ");
            string firstName = Console.ReadLine();

            Console.Write("Unesite prezime kontakta koji se briše: ");
            string lastName = Console.ReadLine();

            Contact contactToDelete = phoneBook.Keys.FirstOrDefault(k => k.FirstName.Equals(firstName, StringComparison.OrdinalIgnoreCase) &&
            k.LastName.Equals(lastName, StringComparison.OrdinalIgnoreCase));

            if (contactToDelete != null)
            {
                while (true)
                {
                    Console.WriteLine($"Potvrđujete li brisanje kontakta {firstName} {lastName} (da/ne)?");
                    string conf = Console.ReadLine();

                    if (conf.ToLower() == "da")
                    {
                        phoneBook.Remove(contactToDelete);
                        Console.WriteLine($"Kontakt {firstName} {lastName} izbrisan iz imenika.");
                    }
                    else if (conf.ToLower() == "ne")
                    {
                        return;
                    }
                    else
                    {
                        Console.WriteLine("Pogrešan unos.");
                        continue;
                    }
                    break;
                } 
            }
            else
            {
                Console.WriteLine("Kontakt nije pronađen.");
            }
            Console.WriteLine();
            Console.WriteLine("Pritisnite bilo koju tipku za povratak na glavni meni: ");
            Console.ReadKey();
        }
        static void EditPrefferences()
        {

        }
        static void PrintAllCalls()
        {

        }

        static void PrintAllContactCalls()
        {

        }
        static void MakeNewCall()
        {

        }
    }
}
