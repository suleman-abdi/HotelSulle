using HotelSulleLibrary.Data;
using HotelSulle.GuestRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSulle.BookingRepository
{
    public class DeleteBooking
    {
        private readonly ApplicationDbContext _dbContext;
        public DeleteBooking(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Delete()
        {
            Console.WriteLine("===========================================================================");
            Console.WriteLine("\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t");
            Console.WriteLine("\t1. Ta bort en bokning");
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
                        Console.WriteLine("Välj Id på den bokning som du vill ta bort");

                        var validBooking = _dbContext.Booking.Where(b => b.IsValid == true).ToList();
                        foreach (var booking in validBooking)
                        {
                            Console.WriteLine($"\nBokning ID: {booking.BookingId}\n");
                            Console.WriteLine($"Bokningperiod: {booking.CheckInDate} - {booking.CheckOutDate}\n");

                        }


                        var bookingIdToDelete = 0;


                        bool validInput = false;

                        while (!validInput)
                        {
                            string userInput = Console.ReadLine();

                            if (int.TryParse(userInput, out bookingIdToDelete))
                            {
                                var bookingToDelete = _dbContext.Booking.FirstOrDefault(b => b.BookingId == bookingIdToDelete);

                                if (bookingToDelete != null)
                                {
                                    bookingToDelete.IsValid = false;
                                    _dbContext.SaveChanges();

                                    Console.WriteLine("Bokningen är inaktiverat!");
                                    Console.WriteLine("Tryck på O för att gå tillbaka till huvudmenyn");

                                    Console.Clear();
                                    var recep = new Reception();
                                    recep.ReceptionMenu();
                                    validInput = true;
                                }
                                else
                                {
                                    Console.WriteLine("Bokningen med det angivna ID:et kunde inte hittas. Ange ett annat ID:");
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
                        Console.WriteLine("Fel inmatning!");
                        break;
                }
            }





        }
    }
}
