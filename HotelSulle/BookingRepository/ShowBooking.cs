using HotelSulleLibrary.Data;
using HotelSulle.GuestRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSulle.BookingRepository
{
    public class ShowBooking
    {
        private readonly ApplicationDbContext _dbContext;
        public ShowBooking(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void DisplayBooking()
        {
            Console.WriteLine("===========================================================================");
            Console.WriteLine("\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t");
            Console.WriteLine("\t1. Se befintliga bokningar");
            Console.WriteLine("\t2. Se obefintliga bokningar");
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
                        var validBooking = _dbContext.Booking.Where(b => b.IsValid == true).ToList();
                        foreach (var booking in validBooking)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("==========================================================================================================");
                            Console.WriteLine($"Bokning ID {booking.BookingId}|| Bokningen gäller från {booking.CheckInDate} till {booking.CheckOutDate}");
                            Console.WriteLine("==========================================================================================================");
                            Console.ResetColor();
                        }
                        Console.WriteLine("Tryck på O för att gå tillbaka till huvudmenyn");

                        break;

                    case "2":
                        var inValidBooking = _dbContext.Booking.Where(b => b.IsValid == false).ToList();
                        if (inValidBooking.Count > 0)
                        {
                            foreach (var booking in inValidBooking)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("==========================================================================================================");
                                Console.WriteLine($"Bokning ID {booking.BookingId}|| Bokningen gällde från {booking.CheckInDate} till {booking.CheckOutDate}");
                                Console.WriteLine("==========================================================================================================");
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
                        var reception = new Reception();
                        reception.ReceptionMenu();
                        break;
                }
            }
        }
    }
}
