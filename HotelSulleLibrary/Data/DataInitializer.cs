using Microsoft.EntityFrameworkCore;

namespace  HotelSulleLibrary.Data
{
    public class DataInitializer
    {
        public void MigrateAndSeed(ApplicationDbContext dbContext)
        {
            dbContext.Database.Migrate();
            LoadData(dbContext);
        }
        public void LoadData(ApplicationDbContext dbContext)
        {
            if (dbContext.Guest.Any()) return;

            var guests = new List<Guest>();
            var rooms = new List<Room>();
            var bookings = new List<Booking>();

            var guest1 = new Guest { GuestFirstName = "Karl", GuestLastName = "Karlsson", Address = "Axbyplan 7", GuestEmail = "Karl.Karlsson@hotmail.com", IsActive = true };
            var guest2 = new Guest { GuestFirstName = "Anders", GuestLastName = "Andersson", Address = "Ljungbyplan 18", GuestEmail = "Anders.Andersson", IsActive = true };
            guests.Add(guest1);
            guests.Add(guest2);
            guests.Add(new Guest { GuestFirstName = "Malin", GuestLastName = "Persson", Address = "Mjölbyplan 11", GuestEmail = "Malin.Persson@hotmail.com", IsActive = false });
            guests.Add(new Guest { GuestFirstName = "Anna", GuestLastName = "Johnsson", Address = "Vimmerbyplan 20", GuestEmail = "Anna.Johnsson@hotmail.com", IsActive = false });

            var room1 = new Room { RoomNumber = 101, TypeOfRoom = "Enkelrum", IsAvailable = false, BookingStartTime = DateTime.Now.Date, BookingEndTime = DateTime.Now.Date.AddDays(2), PricePerNight = 1500 };
            var room2 = new Room { RoomNumber = 102, TypeOfRoom = "Enkelrum", IsAvailable = false, BookingStartTime = DateTime.Now.Date, BookingEndTime = DateTime.Now.Date.AddDays(4), PricePerNight = 1500 };

            rooms.Add(room1);
            rooms.Add(room2);
            rooms.Add(new Room { RoomNumber = 103, TypeOfRoom = "Dubbelrum", IsAvailable = true, PricePerNight = 2000});
            rooms.Add(new Room { RoomNumber = 104, TypeOfRoom = "Dubbelrum", IsAvailable = true, PricePerNight = 2000});

            foreach (var guest in guests)
            {
                dbContext.Guest.Add(guest);
            }

            foreach (var room in rooms)
            {
                dbContext.Room.Add(room);
            }

            dbContext.SaveChanges();

            bookings.Add(new Booking { GuestId = guest1.GuestId, RoomId = room1.RoomId, CheckInDate = DateTime.Now.Date, CheckOutDate = DateTime.Now.Date.AddDays(2), IsPaid = true, Price = 1500, IsValid = true});
            bookings.Add(new Booking { GuestId = guest2.GuestId, RoomId = room2.RoomId, CheckInDate = DateTime.Now.Date, CheckOutDate = DateTime.Now.Date.AddDays(4), IsPaid = true, Price = 1500, IsValid = true });

            foreach (var booking in bookings)
            {
                dbContext.Booking.Add(booking);
            }
            dbContext.SaveChanges();
        }
    }
}
