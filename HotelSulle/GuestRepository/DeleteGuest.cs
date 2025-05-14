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
    public class DeleteGuest
    {
        private readonly ApplicationDbContext _dbContext;
        public DeleteGuest(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Delete()
        {
            Console.WriteLine("===========================================================================");
            Console.WriteLine("\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t");
            Console.WriteLine("\t1. Inaktivera en gäst");
            Console.WriteLine("\t0. Huvudmenyn");
            Console.WriteLine("\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t");
            Console.WriteLine("===========================================================================");
            bool run = false;
            while (!run)
            {
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":

                        var activeGuests = _dbContext.Guest.Where(g => g.IsActive == true).ToList();
                        foreach (var guest in activeGuests)
                        {
                            Console.WriteLine("\n===========================================");
                            Console.WriteLine($"ID: {guest.GuestId}");
                            Console.WriteLine($"Förnamn: {guest.GuestFirstName}");
                            Console.WriteLine($"Efternamn: {guest.GuestLastName}");
                            Console.WriteLine($"Email: {guest.GuestEmail}");
                            Console.WriteLine($"Adress: {guest.Address}");
                            Console.WriteLine("=============================================\n");
                        }

                        Console.WriteLine("Välj Id på den gäst som du vill ta inaktivera");
                        var guestIdToDelete = 0;
                        bool validInput = false;

                        while (!validInput)
                        {
                            string userInput = Console.ReadLine();

                            if (int.TryParse(userInput, out guestIdToDelete))
                            {
                                var guestToDelete = _dbContext.Guest.FirstOrDefault(g => g.GuestId == guestIdToDelete);

                                if (guestToDelete != null)
                                {
                                    guestToDelete.IsActive = false;
                                    _dbContext.SaveChanges();

                                    Console.WriteLine("Gästen är inaktiverat!");
                                    Console.WriteLine("Tryck på O för att gå tillbaka till huvudmenyn");
                                    Console.ReadLine();

                                    Console.Clear();
                                    var recep = new Reception();
                                    recep.ReceptionMenu();
                                    validInput = true;
                                }
                                else
                                {
                                    Console.WriteLine("Gästen med det angivna ID:et kunde inte hittas. Ange ett annat ID:");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Inmatningen är ogiltig. Vänligen ange ett nummer:");
                            }
                        }
                        break;

                    case "0":

                        Console.Clear();
                        var reception = new Reception();
                        reception.ReceptionMenu();
                        break;

                    default:
                        Console.WriteLine("Fel val! Välj igen");
                        break;
                }
            }
        }
    }
}
