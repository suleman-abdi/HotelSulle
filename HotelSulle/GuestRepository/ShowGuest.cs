using HotelSulleLibrary.Data;
using HotelSulle.GuestRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace HotelSulle.GuestRepository
{
    public class ShowGuest
    {
        private readonly ApplicationDbContext _dbContext;

        public ShowGuest(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void DisplayGuest()
        {

            Console.WriteLine("===========================================================================");
            Console.WriteLine("\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t");
            Console.WriteLine("\t1. Se aktiva gäster");
            Console.WriteLine("\t2. Se inaktiva gäster");
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
                        var activeGuests = _dbContext.Guest.Where(g => g.IsActive == true).ToList();
                        foreach (var guest in activeGuests)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"\nID: {guest.GuestId}");
                            Console.WriteLine($"\nFörnamn: {guest.GuestFirstName}");
                            Console.WriteLine($"Efternamn: {guest.GuestLastName}");
                            Console.WriteLine($"Email: {guest.GuestEmail}");
                            Console.WriteLine($"Address: {guest.Address}\n");
                            Console.ResetColor();
                        }
                        Console.WriteLine("Tryck på O för att gå tillbaka till huvudmenyn");

                        break;

                    case "2":

                        var inActiveGuests = _dbContext.Guest.Where(g => g.IsActive == false).ToList();
                        if (inActiveGuests.Count > 0)
                        {
                            foreach (var guest in inActiveGuests)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine($"\nID: {guest.GuestId}");
                                Console.WriteLine($"\nFörnamn: {guest.GuestFirstName}");
                                Console.WriteLine($"Efternamn: {guest.GuestLastName}");
                                Console.WriteLine($"Email: {guest.GuestEmail}");
                                Console.WriteLine($"Address: {guest.Address}\n");
                                Console.ResetColor();
                            }
                            Console.WriteLine("Tryck på O för att gå tillbaka till huvudmenyn");

                        }

                        else
                        {
                            Console.WriteLine("\nDet finns inga inaktiva gäster.");
                            Console.WriteLine("Tryck på O för att gå tillbaka till huvudmenyn");

                        }

                        break;

                    case "0":
                        Console.Clear();
                        var backTo = new GuestMenu();
                        backTo.GuestMenuChoice();
                        run = false;
                        break;

                    default:
                        Console.WriteLine("Fel inmatning!");
                        break;
                }

            }

        }

    }
}
