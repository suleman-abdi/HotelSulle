using HotelSulleLibrary.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSulle.GuestRepository
{
    public class CreateGuest
    {
        private readonly ApplicationDbContext _dbContext;


        public CreateGuest(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public void CreateNewGuest()
        {

            var guest = new Guest();

            Console.WriteLine("===========================================================================");
            Console.WriteLine("\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t");
            Console.WriteLine("\t1. Registrera ny gäst");
            Console.WriteLine("\t0. Huvudmenyn");
            Console.WriteLine("\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t");
            Console.WriteLine("===========================================================================");

            bool run = true;

            while (run)
            {
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        string guestFirstName;
                        string guestLastName;
                        string guestEmail;
                        string address;

                        while (true)
                        {
                            Console.Write("nAnge gästens förnamn: ");
                            guestFirstName = Console.ReadLine();

                            Console.Write("Ange gästens efternamn: ");
                            guestLastName = Console.ReadLine();

                            Console.Write("Ange gästens e-postadress: \n");
                            guestEmail = Console.ReadLine();

                            Console.Write("Ange gästens adress: ");
                            address = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(guestFirstName) || string.IsNullOrWhiteSpace(guestLastName) || string.IsNullOrWhiteSpace(guestEmail) || string.IsNullOrWhiteSpace(address))
                            {
                                Console.WriteLine("\nOgiltigt, försök igen. Alla fält måste fyllas i\n.");
                                continue;

                            }

                            if (!guestFirstName.All(char.IsLetter) || !guestLastName.All(char.IsLetter))
                            {
                                Console.WriteLine("\nOgiltigt, försök igen. Förnamn och efternamn får endast innehålla bokstäver. Försök igen\n");
                                continue;
                            }
                            break;
                        }


                        guest.GuestFirstName = guestFirstName;
                        guest.GuestLastName = guestLastName;
                        guest.GuestEmail = guestEmail;
                        guest.Address = address;

                        _dbContext.Guest.Add(guest);
                        _dbContext.SaveChanges();

                        Console.WriteLine("\n\nGästen är registrerad!\n\n");
                        Console.WriteLine("\nTryck på O för att gå tillbaka till huvudmenyn");


                        Console.Clear();
                        var rec = new Reception();
                        rec.ReceptionMenu();
                        break;

                    case "0":
                        Console.Clear();
                        var reception = new Reception();
                        reception.ReceptionMenu();
                        break;

                    default:
                        Console.WriteLine("\nFel inmatning!");
                        break;
                }
            }
        }
    }
}











