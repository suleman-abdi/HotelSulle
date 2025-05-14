using HotelSulleLibrary.Data;
using System.Net;

namespace HotelSulle.BookingRepository
{
    public class CreateBooking
    {
        private readonly ApplicationDbContext _dbContext;

        public CreateBooking(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;

        }
        public void NewBooking()
        {

            var booking = new Booking();

            Console.WriteLine("Ny bokning                 ||Enkelrum/kväll 1500SEK||  ||Dubbelrum per kväll 20000SEK||");

            Console.WriteLine("===========================================================================");
            Console.WriteLine("\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t");
            Console.WriteLine("\t1. Gör en bokning");
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
                        Console.WriteLine($"\nGäster\n");

                        var inActiveGuests =
                          (from g in _dbContext.Guest

                           where g.IsActive == false
                           select g).ToList();

                        if (inActiveGuests.Count == 0)
                        {
                            Console.WriteLine("Det finns inga gäster att boka!");
                            Console.ReadLine();
                            Console.Clear();
                            return;
                        }

                        foreach (var guest in inActiveGuests)
                        {
                            Console.WriteLine("\n===========================================================================");
                            Console.WriteLine($"ID: {guest.GuestId}\nNamn: {guest.GuestLastName}, {guest.GuestFirstName}");
                            Console.WriteLine("===========================================================================\n");
                        }
                        Console.Write("\nAnge gästens Id: ");

                        int guestId = 0;

                        var guestIdToBook = new Guest();

                        while (true)
                        {
                            if (!int.TryParse(Console.ReadLine(), out guestId))
                            {
                                Console.WriteLine("Fel inmatning! Ange ett giltigt nummer.");
                            }
                            guestIdToBook = _dbContext.Guest.FirstOrDefault(g => g.GuestId == guestId);

                            if (guestIdToBook != null)
                            {
                                booking.GuestId = guestIdToBook.GuestId;
                                break;
                            }
                            Console.WriteLine("Gästen existerar inte! Ange ett giltigt rum Id");
                        }

                        Console.WriteLine("==============================================================================\n");
                        Console.WriteLine($"Samtliga rum\n");

                        var availableRooms =
                         (from r in _dbContext.Room

                          where r.IsAvailable == true
                          select r).ToList();

                        foreach (var room in availableRooms)
                        {
                            Console.WriteLine("\n===========================================================================");
                            Console.WriteLine($"ID: {room.RoomId}\nNummer: {room.RoomNumber}\nTyp av rum:{room.TypeOfRoom}");
                            Console.WriteLine("============================================================================\n");
                        }

                        if (availableRooms.Count == 0)
                        {
                            Console.WriteLine("Det finns inga rum att boka!");
                            Console.ReadLine();
                            Console.Clear();
                            return;
                        }

                        Console.WriteLine("\nAnge rummets Id: ");

                        int roomId = 0;


                        var roomIdToBook = new Room();

                        while (true)
                        {
                            if (!int.TryParse(Console.ReadLine(), out roomId))
                            {
                                Console.WriteLine("Fel inmatning! Ange ett giltigt nummer.");
                            }
                            roomIdToBook = _dbContext.Room.FirstOrDefault(r => r.RoomId == roomId);

                            if (roomIdToBook != null)
                            {
                                if (_dbContext.Booking.Any(b => b.RoomId == roomIdToBook.RoomId))
                                {
                                    Console.WriteLine("Det valda rummet har redan en aktiv bokning. Klicka enter");
                                    Console.ReadLine();
                                    Console.Clear();
                                    return;
                                }
                                booking.RoomId = roomIdToBook.RoomId;
                                break;
                            }
                            Console.WriteLine("Rummet existerar inte! Ange ett giltigt rum Id");
                        }

                       
                            roomIdToBook.IsAvailable = false;
                            booking.CheckInDate = roomIdToBook.BookingStartTime;
                            booking.CheckOutDate = roomIdToBook.BookingEndTime;
                        

                   
                        if (roomIdToBook.TypeOfRoom.ToLower() == "dubbelrum")
                        {
                            Console.WriteLine("Ange antal extra sängar (1 eller 2): ");
                            int extraBeds = 0;

                            while (!int.TryParse(Console.ReadLine(), out extraBeds))
                            {
                                Console.WriteLine("Fel inmatning!");
                            }

                            if (roomIdToBook.ExtraBeds == 2)
                            {
                                Console.WriteLine("Rummet har nått maxgränsen på extra sängar");
                                
                            }

                            roomIdToBook.ExtraBeds = extraBeds;

                            Console.WriteLine("\nAnge antal nätter för bokningen:");
                            var numberOfNights = 0;


                            while (!int.TryParse(Console.ReadLine(), out numberOfNights) || numberOfNights <= 0)
                            {
                                Console.WriteLine("Inmatningen är ogiltig. Ange ett positivt heltal för antal nätter.");
                            }

                            booking.CheckInDate = DateTime.Now.Date;

                            booking.CheckOutDate = booking.CheckInDate.AddDays(numberOfNights);

                            roomIdToBook.BookingStartTime = DateTime.Now.Date;

                            roomIdToBook.BookingEndTime = roomIdToBook.BookingStartTime.AddDays(numberOfNights);

                            var priceToPay = numberOfNights * roomIdToBook.PricePerNight;

                            booking.Price = (double)priceToPay;

                            Console.WriteLine($"\nPris: {priceToPay} SEK");
                            Console.WriteLine("\nTryck 1 för att betala");
                            string payMent = Console.ReadLine();

                            while (payMent != "1")
                            {
                                Console.WriteLine("Ogiltigt val. Tryck 1 för att betala");
                                payMent = Console.ReadLine();
                            }

                            if (string.IsNullOrWhiteSpace(payMent))
                            {
                                Console.WriteLine("Ogiltigt, försök igen.");
                            }


                            if (payMent == "1")
                            {
                                booking.IsPaid = true;
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("\nBetalningen är genomförd!$$$");
                                Console.ResetColor();



                            }

                            else if (payMent != "1")
                            {
                                booking.IsPaid = false;
                            }

                            if (string.IsNullOrWhiteSpace(payMent))
                            {
                                Console.WriteLine("Ogiltigt.");
                            }

                            booking.IsValid = true;
                            _dbContext.Booking.Add(booking);
                            _dbContext.SaveChanges();

                            Console.WriteLine($"\nBokning skapad för {guestIdToBook.GuestFirstName} {guestIdToBook.GuestLastName} och rum {roomIdToBook.RoomNumber} från {booking.CheckInDate.ToShortDateString()} - {booking.CheckOutDate.ToShortDateString()}.");
                            Console.WriteLine("Tryck på O för att gå tillbaka till huvudmenyn");
                            Console.ReadLine();

                            Console.Clear();
                            var r = new Reception();
                            r.ReceptionMenu();
                        }





                        if (roomIdToBook.TypeOfRoom.ToLower() == "enkelrum")
                        {
                            Console.WriteLine("\nAnge antal nätter för bokningen:");
                            var numberOfNightss = 0;

                            while (!int.TryParse(Console.ReadLine(), out numberOfNightss) || numberOfNightss <= 0)
                            {
                                Console.WriteLine("Inmatningen är ogiltig. Ange ett positivt heltal för antal nätter.");
                            }

                            booking.CheckInDate = DateTime.Now.Date;

                            booking.CheckOutDate = booking.CheckInDate.AddDays(numberOfNightss);

                            roomIdToBook.BookingStartTime = DateTime.Now.Date;

                            roomIdToBook.BookingEndTime = roomIdToBook.BookingStartTime.AddDays(numberOfNightss);

                            var priceToPayy = numberOfNightss * roomIdToBook.PricePerNight;

                            booking.Price = (double)priceToPayy;

                            Console.WriteLine($"\nPris: {priceToPayy} SEK");
                            Console.WriteLine("\nTryck 1 för att betala");
                            string payMentt = Console.ReadLine();

                            while (payMentt != "1")
                            {
                                Console.WriteLine("Ogiltigt val. Tryck 1 för att betala");
                                payMentt = Console.ReadLine();
                            }

                            if (string.IsNullOrWhiteSpace(payMentt))
                            {
                                Console.WriteLine("Ogiltigt, försök igen.");
                            }


                            if (payMentt == "1")
                            {
                                booking.IsPaid = true;
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("\nBetalningen är genomförd!$$$");
                                Console.ResetColor();
                            }

                            else if (payMentt != "1")
                            {
                                booking.IsPaid = false;
                            }

                            if (string.IsNullOrWhiteSpace(payMentt))
                            {
                                Console.WriteLine("Ogiltigt.");
                            }
                            booking.IsValid = true;
                            _dbContext.Booking.Add(booking);
                            _dbContext.SaveChanges();


                            Console.WriteLine($"\nBokning skapad för {guestIdToBook.GuestFirstName} {guestIdToBook.GuestLastName} och rum {roomIdToBook.RoomNumber} från {booking.CheckInDate.ToShortDateString()} till {booking.CheckOutDate.ToShortDateString()}.");
                            Console.WriteLine("Tryck på O för att gå tillbaka till huvudmenyn");
                            Console.ReadLine();

                            Console.Clear();
                            var rc = new Reception();
                            rc.ReceptionMenu();
                        }

                        break;



                        Console.Clear();
                        var rec = new Reception();
                        rec.ReceptionMenu();
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
