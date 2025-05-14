using HotelSulle.BookingRepository;
using HotelSulleLibrary.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HotelSulle.GuestRepository
{
    public class UpdateGuest
    {
        private readonly ApplicationDbContext _dbContext;
        public UpdateGuest(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Update()
        {

            Console.WriteLine("===========================================================================");
            Console.WriteLine("\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t");
            Console.WriteLine("\t1. Uppdatera gäst");
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
                        foreach (var guest in _dbContext.Guest)
                        {
                            Console.WriteLine($"Id: {guest.GuestId}");
                            Console.WriteLine($"Förnamn: {guest.GuestFirstName}");
                            Console.WriteLine($"Efternamn: {guest.GuestLastName}");
                            Console.WriteLine($"E-mail: {guest.GuestEmail}");
                            Console.WriteLine($"Adress: {guest.Address}");
                            Console.WriteLine("====================");
                        }



                        Console.WriteLine("Välj Id på den gäst som du vill uppdatera");
                        int guestId = 0;

                        var guestToUpdate = new Guest();

                        while (true)
                        {
                            if (!int.TryParse(Console.ReadLine(), out guestId))
                            {
                                Console.WriteLine("Fel inmatning! Ange ett giltigt nummer.");
                            }
                            guestToUpdate = _dbContext.Guest.FirstOrDefault(g => g.GuestId == guestId);

                            if (guestToUpdate != null)
                            {
                                break;
                            }
                            Console.WriteLine("Gästen existerar inte! Ange ett giltigt rum Id");
                        }

                        Console.Clear();


                        Console.WriteLine("Ange förnamn: ");
                        var guestFirstNameUpdate = Console.ReadLine();

                        Console.WriteLine("Ange efternamn: ");
                        var guestLastNameUpdate = Console.ReadLine();

                        Console.WriteLine("Ange e-mail: ");
                        var guestEmailUpdate = Console.ReadLine();

                        Console.WriteLine("Ange adress: ");
                        var guestAddressUpdate = Console.ReadLine();

                        if (string.IsNullOrWhiteSpace(guestFirstNameUpdate) || string.IsNullOrWhiteSpace(guestLastNameUpdate) || string.IsNullOrWhiteSpace(guestEmailUpdate) || string.IsNullOrWhiteSpace(guestAddressUpdate))
                        {
                            Console.WriteLine("Ogiltigt, försök igen. Alla fält måste fyllas i.");
                            Console.ReadLine();
                            Console.Clear();
                            return;
                        }

                        if (!guestFirstNameUpdate.All(char.IsLetter) || !guestLastNameUpdate.All(char.IsLetter))
                        {
                            Console.WriteLine("Ogiltigt, försök igen. Förnamn och efternamn får endast innehålla bokstäver. Klicka enter");
                            Console.ReadLine();
                            Console.Clear();
                            return;
                        }
                        guestToUpdate.GuestFirstName = guestFirstNameUpdate;
                        guestToUpdate.GuestLastName = guestLastNameUpdate;
                        guestToUpdate.GuestEmail = guestEmailUpdate;
                        guestToUpdate.Address = guestAddressUpdate;

                        _dbContext.SaveChanges();

                        Console.WriteLine("Uppdateringen är genomförd!");
                        Console.WriteLine("Tryck på O för att gå tillbaka till huvudmenyn");

                        Console.Clear();
                        var reception = new Reception();
                        reception.ReceptionMenu();


                        break;

                    case "0":

                        Console.Clear();
                        var rec = new Reception();
                        rec.ReceptionMenu();
                        break;

                    default:
                        Console.WriteLine("Välj ett av alternativen!");
                        break;
                }
            }
        }
    }
}
