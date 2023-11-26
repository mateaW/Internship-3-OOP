using PhoneBookApp.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.ComponentModel.Design;
using System.ComponentModel;

namespace PhoneBookApp
{
    public class Program
    {
        static readonly Random random = new Random();
        static readonly Dictionary<Contact, List<Call>> phoneBook = new Dictionary<Contact, List<Call>>()
        {
            {
                new Contact("Marta", "Katić", "091 100 4429", Prefference.Blocked), new List<Call>()
                {
                new Call(new DateTime(2023, 04, 08, 12, 22, 00), Status.Finished)
                }
            },
            {
                new Contact("Marin", "Cecić", "091 234 5678", Prefference.Favourite), new List<Call>()
                {
                    new Call(new DateTime(2023, 11, 25, 12, 30, 00), Status.Finished),
                    new Call(new DateTime(2023, 11, 20, 10, 12, 05), Status.Finished)
                }
            },
            {
                new Contact("Marta", "Batinić", "095 539 7809", Prefference.Normal), new List<Call>()
                {
                    new Call(new DateTime(2023, 10, 05, 16, 22, 50), Status.Missed)
                }
            },
            {
                new Contact("Rina", "Miočić", "091 329 1312", Prefference.Normal), new List<Call>()
                {
                    new Call(new DateTime(2023, 11, 02, 20, 34, 20, 09), Status.Finished),
                    new Call(new DateTime(2023, 11, 15, 22, 00, 45), Status.Finished),
                    new Call(new DateTime(2023, 11, 16, 6, 17, 30), Status.Missed)
                }
            },
            {
                new Contact("Stipe", "Bilonić", "092 234 7799", Prefference.Favourite), new List<Call>()
                {
                    new Call(new DateTime(2023, 11, 01, 10, 08, 09), Status.Finished),
                    new Call(new DateTime(2023, 08, 12, 17, 38, 49), Status.Finished)
                }
            }
        };

        static void Main()
        {
            while (true)
            {
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
                        Console.WriteLine();
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
                        Console.Clear();
                        Console.WriteLine("Pogrešan unos. Upišite brojeve 1-3.");
                        Console.WriteLine();
                        break;
                }
            }
        }

        static void PrintAllContacts()
        {
            Console.WriteLine("----SVI KONTAKTI----");
            Console.WriteLine();

            DispalyContacts();
             
            Console.WriteLine();
            Console.WriteLine("Pritisnite bilo koju tipku za povratak na glavni meni... ");
            Console.ReadKey();
            Console.Clear();
        }

        static void AddNewContacts()
        {
            var newFirstName = "";
            var newLastName = "";
            var newPhoneNumber = "";
            Prefference newPrefference = new Prefference();

            Console.WriteLine("----UNOS NOVOG KONTAKTA----");
            Console.WriteLine();

            while (true)
            {
                Console.Write("Unesite ime novog kontakta: ");
                newFirstName = Console.ReadLine();

                if (newFirstName == "")
                {
                    Console.WriteLine("Ime ne smije biti prazno.");
                    continue;
                }

                Console.Write("Unesite prezime novog kontakta: ");
                newLastName = Console.ReadLine(); // lastName can be empty

                bool contactExists = phoneBook.Keys.Any(k => k.FirstName.Equals(newFirstName, StringComparison.OrdinalIgnoreCase) && k.LastName.Equals(newLastName, StringComparison.OrdinalIgnoreCase));

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
                newPhoneNumber = Console.ReadLine();

                if (phoneBook.Keys.Any(contact => contact.PhoneNumber.Equals(newPhoneNumber, StringComparison.OrdinalIgnoreCase)))
                {
                    Console.WriteLine("Kontakt s istim brojem mobitela već postoji.");
                    continue;
                }

                if (newPhoneNumber == "")
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
                        newPrefference = Prefference.Favourite;
                        break;
                    case "2":
                        newPrefference = Prefference.Normal;
                        break;
                    case "3":
                        newPrefference = Prefference.Blocked;
                        break;
                    default:
                        Console.WriteLine("Pogrešan unos za preferencu. Upišite brojeve 1-3.");
                        continue;
                }
                break;
            }
            while (true)
            {
                Console.WriteLine($"Potvrđujete li unos kontakta {newFirstName} {newLastName} (da/ne)?");
                string confirmation = Console.ReadLine();

                if (confirmation.ToLower() == "da")
                {
                    Contact newContact = new Contact(newFirstName, newLastName, newPhoneNumber, newPrefference);
                    phoneBook.Add(newContact, new List<Call>());

                    Console.WriteLine($"Novi kontakt '{newFirstName} {newLastName} - {newPhoneNumber} - {newPrefference}' dodan u imenik.");
                }
                else if (confirmation.ToLower() == "ne")
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
            Console.WriteLine("Pritisnite bilo koju tipku za povratak na glavni meni... ");
            Console.ReadKey();
            Console.Clear();
        }

        static void DispalyContacts()
        {
            for (int i = 0; i < phoneBook.Count(); i++)
            {
                Contact contact = phoneBook.Keys.ElementAt(i);
                Console.WriteLine($"{i + 1}. {contact.FirstName} {contact.LastName} - {contact.PhoneNumber} - {contact.Prefference}");
            }
        }

        static void DeleteContact()
        {
            Console.WriteLine("----BRISANJE KONTAKTA----");           
            Console.WriteLine();

            DispalyContacts(); //so the user can see which ones to delete
            Console.WriteLine();

            Console.Write("Unesite ime kontakta koji se briše: ");
            string firstNameToDelete = Console.ReadLine();

            Console.Write("Unesite prezime kontakta koji se briše: ");
            string lastNameToDelete = Console.ReadLine();

            Contact contactToDelete = phoneBook.Keys.FirstOrDefault(k => k.FirstName.Equals(firstNameToDelete, StringComparison.OrdinalIgnoreCase) &&
            k.LastName.Equals(lastNameToDelete, StringComparison.OrdinalIgnoreCase));

            if (contactToDelete != null)
            {
                while (true)
                {
                    Console.WriteLine($"Potvrđujete li brisanje kontakta '{contactToDelete.FirstName} {contactToDelete.LastName} - {contactToDelete.PhoneNumber} - {contactToDelete.Prefference}' (da/ne)?");
                    string confirmation = Console.ReadLine();

                    if (confirmation.ToLower() == "da")
                    {
                        phoneBook.Remove(contactToDelete);
                        Console.WriteLine($"Kontakt izbrisan.");
                    }
                    else if (confirmation.ToLower() == "ne")
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
            Console.WriteLine("Pritisnite bilo koju tipku za povratak na glavni meni... ");
            Console.ReadKey();
            Console.Clear();
        }
        static void EditPrefferences()
        {
            Console.WriteLine("----PROMJENA PREFERENCI KONTAKTA----");
            Console.WriteLine();

            DispalyContacts();
            Console.WriteLine();

            Console.Write("Unesite ime kontakta kojem želite promijeniti preferencu: ");
            string firstNameNewPrefference = Console.ReadLine();

            Console.Write("Unesite prezime kontakta kojem želite promijeniti preferencu: ");
            string lastNameNewPrefference = Console.ReadLine();

            Contact contactToChangePrefference = phoneBook.Keys.FirstOrDefault(k => k.FirstName.Equals(firstNameNewPrefference, StringComparison.OrdinalIgnoreCase) &&
            k.LastName.Equals(lastNameNewPrefference, StringComparison.OrdinalIgnoreCase));

            Prefference newPrefference;

            if(contactToChangePrefference != null)
            {
                Console.WriteLine($"Trenutna preferenca kontakta: {contactToChangePrefference.Prefference}");

                while (true)
                {
                    Console.WriteLine("Odaberite broj ispred nove preference kontakta: ");
                    Console.WriteLine("1. Favorit");
                    Console.WriteLine("2. Normalan");
                    Console.WriteLine("3. Blokiran");

                    string option = Console.ReadLine();

                    switch (option)
                    {
                        case "1":
                            newPrefference = Prefference.Favourite;
                            break;
                        case "2":
                            newPrefference = Prefference.Normal;
                            break;
                        case "3":
                            newPrefference = Prefference.Blocked;
                            break;
                        default:
                            Console.WriteLine("Pogrešan unos za preferencu. Upišite brojeve 1-3.");
                            continue;
                    }
                    break;
                }
                while (true)
                {
                    Console.WriteLine($"Potvrđujete li promjenu preference kontakta {contactToChangePrefference.FirstName} {contactToChangePrefference.LastName} " +
                        $"iz {contactToChangePrefference.Prefference} u {newPrefference} (da/ne)?");

                    string confirmation = Console.ReadLine();

                    if (confirmation.ToLower() == "da")
                    {
                        contactToChangePrefference.Prefference = newPrefference;
                        Console.WriteLine($"Preferenca uspješno promijenjena.");
                    }
                    else if (confirmation.ToLower() == "ne")
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
            Console.WriteLine("Pritisnite bilo koju tipku za povratak na glavni meni... ");
            Console.ReadKey();
            Console.Clear();
        }

        static void PrintAllContactCalls()
        {
            Console.WriteLine("----ISPIS SVIH POZIVA SA KONTAKTOM----");
            Console.WriteLine();

            DispalyContacts();
            Console.WriteLine();

            Console.Write("Unesite ime kontakta čiji popis poziva želite vidjeti: ");
            string firstName = Console.ReadLine();

            Console.Write("Unesite prezime kontakta čiji popis poziva želite vidjeti: ");
            string lastName = Console.ReadLine();

            Console.WriteLine();

            Contact chosenContact = phoneBook.Keys.FirstOrDefault(k => k.FirstName.Equals(firstName, StringComparison.OrdinalIgnoreCase) && k.LastName.Equals(lastName, StringComparison.OrdinalIgnoreCase));

            if (chosenContact != null)
            {
                List<Call> chosenContactCalls = phoneBook[chosenContact];

                chosenContactCalls = chosenContactCalls.OrderByDescending(p => p.CallTime).ToList();

                Console.WriteLine($"POZIVI SA KONTAKTOM: {chosenContact.FirstName} {chosenContact.LastName}: ");
                foreach (var poziv in chosenContactCalls)
                {
                    Console.WriteLine($"Vrijeme poziva: {poziv.CallTime}, Status: {poziv.Status}");
                }
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("Kontakt nije pronađen.");
            }
            Console.WriteLine();
            Console.WriteLine("Pritisnite bilo koju tipku za povratak na submenu... ");
            Console.ReadKey();
            Console.Clear();
        }

        static void MakeNewCall()
        {
            Console.WriteLine("----KREIRANJE NOVOG POZIVA----");
            Console.WriteLine();

            DispalyContacts();
            Console.WriteLine();

            Console.Write("Unesite ime kontakta kojeg želite pozvati: ");
            string firstName = Console.ReadLine();

            Console.Write("Unesite prezime kontakta kojeg želite pozvati: ");
            string lastName = Console.ReadLine();

            Contact contactToCall = phoneBook.Keys.FirstOrDefault(k => k.FirstName.Equals(firstName, StringComparison.OrdinalIgnoreCase) && k.LastName.Equals(lastName, StringComparison.OrdinalIgnoreCase));

            if (contactToCall != null)
            {
                if (!phoneBook.Values.Any(pozivi => pozivi.Any(p => p.Status == Status.InProgress))) //postoji li vec neki poziv u tijeku
                {
                    if (contactToCall.Prefference != Prefference.Blocked) //je li kontakt blokiran
                    {
                        Console.Clear();
                        Console.WriteLine("Pozivanje... ");
                        System.Threading.Thread.Sleep(random.Next(1000, 10000)); // čekamo random vrijeme da se korisnik javi
                        var answerToCall = random.Next(1, 3); // 1 ako je poziv odbijen, 2 ako se korisnik javio
                        if (answerToCall == 2)
                        {
                            int duration = random.Next(1, 21); //random trajanje poziva
                            DateTime endTime = DateTime.Now.AddSeconds(duration); 
                            Console.WriteLine($"Poziv uspostavljen s kontaktom {contactToCall.FirstName} {contactToCall.LastName}.");
                            Console.WriteLine("Poziv...");
                            Thread.Sleep(duration*1000); // cekamo dok traje poziv
                            Console.WriteLine($"Kraj poziva: {endTime}. Trajanje: {duration} sekundi. ");
                            Call newCall = new Call(DateTime.Now, Status.Finished);
                            phoneBook[contactToCall].Add(newCall);
                        }
                        else
                        {
                            Console.WriteLine($"Poziv nije uspostavljen. Kontakt {contactToCall.FirstName} {contactToCall.LastName} je odbio poziv.");
                            Call newCall = new Call(DateTime.Now, Status.Missed);
                            phoneBook[contactToCall].Add(newCall);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Nije moguće uspostaviti poziv s blokiranim kontaktom.");
                    }
                }
                else
                {
                    Console.WriteLine("Već postoji poziv u tijeku. Molimo pričekajte da završi.");
                }
            }
            else
            {
                Console.WriteLine("Kontakt nije pronađen.");
            }
            Console.WriteLine();
            Console.WriteLine("Pritisnite bilo koju tipku za povratak na submenu... ");
            Console.ReadKey();
            Console.Clear();
        }

        static void PrintAllCalls()
        {
            Console.WriteLine("-----SVI POZIVI-----");
            Console.WriteLine();

            foreach (var contact in phoneBook.Keys)
            {
                Console.WriteLine($"Kontakt: {contact.FirstName} {contact.LastName}");
                foreach (var poziv in phoneBook[contact])
                {
                    Console.WriteLine($"Vrijeme poziva: {poziv.CallTime}, Status: {poziv.Status}");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine("Pritisnite bilo koju tipku za povratak na submenu... ");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
