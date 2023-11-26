﻿using PhoneBookApp.Enums;
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
        static Random random = new Random();
        static Dictionary<Contact, List<Call>> phoneBook = new Dictionary<Contact, List<Call>>()
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

        static void Main(string[] args)
        {
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
                Console.Clear();
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
            Console.WriteLine("Pritisnite bilo koju tipku za povratak na glavni meni... ");
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
            Console.WriteLine("Pritisnite bilo koju tipku za povratak na glavni meni... ");
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
            Console.WriteLine("Pritisnite bilo koju tipku za povratak na glavni meni... ");
            Console.ReadKey();
        }
        static void EditPrefferences()
        {
            Console.WriteLine("----PROMJENA PREFERENCI KONTAKTA----");

            for (int i = 0; i < phoneBook.Count(); i++)
            {
                Contact contact = phoneBook.Keys.ElementAt(i);
                Console.WriteLine($"{i + 1}. {contact.FirstName} {contact.LastName} - {contact.PhoneNumber} - {contact.Prefference}");
            }

            Console.WriteLine();

            Console.Write("Unesite ime kontakta kojem želite promijeniti preferencu: ");
            string firstName = Console.ReadLine();

            Console.Write("Unesite prezime kontakta kojem želite promijeniti preferencu: ");
            string lastName = Console.ReadLine();

            Contact contactToChangePrefference = phoneBook.Keys.FirstOrDefault(k => k.FirstName.Equals(firstName, StringComparison.OrdinalIgnoreCase) &&
            k.LastName.Equals(lastName, StringComparison.OrdinalIgnoreCase));

            Prefference preff;

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
                    Console.WriteLine($"Potvrđujete li promjenu preference kontakta {firstName} {lastName} (da/ne)?");
                    string conf = Console.ReadLine();

                    if (conf.ToLower() == "da")
                    {
                        contactToChangePrefference.Prefference = preff;
                        Console.WriteLine($"Preferenca kontakta {firstName} {lastName} uspješno promijenjena.");
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
            Console.WriteLine("Pritisnite bilo koju tipku za povratak na glavni meni... ");
            Console.ReadKey();
        }

        static void PrintAllContactCalls()
        {
            Console.WriteLine("----ISPIS SVIH POZIVA SA KONTAKTOM----");
            Console.WriteLine();

            for (int i = 0; i < phoneBook.Count(); i++)
            {
                Contact contact = phoneBook.Keys.ElementAt(i);
                Console.WriteLine($"{i + 1}. {contact.FirstName} {contact.LastName} - {contact.PhoneNumber} - {contact.Prefference}");
            }

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
        }
        static void MakeNewCall()
        {
            Console.WriteLine("----KREIRANJE NOVOG POZIVA----");
            Console.WriteLine();

            for (int i = 0; i < phoneBook.Count(); i++)
            {
                Contact contact = phoneBook.Keys.ElementAt(i);
                Console.WriteLine($"{i + 1}. {contact.FirstName} {contact.LastName} - {contact.PhoneNumber} - {contact.Prefference}");
            }
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
                    if (contactToCall.Prefference != Prefference.Blocked) // je li kontakt blokiran
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
        }
    }
}
