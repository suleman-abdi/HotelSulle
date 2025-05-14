using HotelSulleLibrary.Data;
using HotelSulle.GuestRepository;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HotelSulle.BookingRepository
{
    public class UpdateBooking
    {
        private readonly ApplicationDbContext _dbContext;
        public UpdateBooking(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Update()
        {
            Console.WriteLine("===========================================================================");
            Console.WriteLine("\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t");
            Console.WriteLine("\t1. Uppdatera bokning");
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
                        foreach (var booking in _dbContext.Booking)
                        {
                            Console.WriteLine("\n==========================================");
                            Console.WriteLine($"Id: {booking.BookingId}");
                            Console.WriteLine($"Incheckning: {booking.CheckInDate}");
                            Console.WriteLine($"Utcheckning: {booking.CheckOutDate}");
                            Console.WriteLine("============================================\n");

                        }


                        Console.WriteLine("Välj Id på den bokningen som du vill uppdatera");
                        int bookingId = 0;

                        var bookingToUpdate = new Booking();

                        while (true)
                        {
                            if (!int.TryParse(Console.ReadLine(), out bookingId))
                            {
                                Console.WriteLine("Fel inmatning! Ange ett giltigt nummer.");
                            }
                            bookingToUpdate = _dbContext.Booking.FirstOrDefault(b => b.BookingId == bookingId);

                            if (bookingToUpdate != null)
                            {
                                break;
                            }
                            Console.WriteLine("Bokningen existerar inte! Ange ett giltigt rum Id");
                        }



                        Console.WriteLine("Ange nya incheckningsdatum (yyyy-MM-dd): ");
                        string checkInDateString = Console.ReadLine();

                        Console.WriteLine("Ange nya utcheckningsdatum (yyyy-MM-dd): ");
                        string checkOutDateString = Console.ReadLine();


                        if (DateTime.TryParseExact(checkInDateString, "yyyy-MM-dd", null, DateTimeStyles.None, out DateTime bookingCheckInDateToUpdate)
                            && DateTime.TryParseExact(checkOutDateString, "yyyy-MM-dd", null, DateTimeStyles.None, out DateTime bookingCheckOutDateToUpdate))
                        {
                            bookingToUpdate.CheckInDate = bookingCheckInDateToUpdate;
                            bookingToUpdate.CheckOutDate = bookingCheckOutDateToUpdate;

                            _dbContext.SaveChanges();

                            Console.WriteLine("Uppdateringen är genomförd!");
                            Console.WriteLine("Tryck på O för att gå tillbaka till huvudmenyn");

                            Console.Clear();
                            var rec = new Reception();
                            rec.ReceptionMenu();
                        }
                        else
                        {
                            Console.WriteLine("Ogiltigt datumformat! Klicka enter");
                            Console.ReadLine();
                            Console.Clear();
                            return;
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
