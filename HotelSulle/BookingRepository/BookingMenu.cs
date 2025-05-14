using HotelSulleLibrary.Data;
using Microsoft.EntityFrameworkCore;

namespace HotelSulle.BookingRepository
{
    public class BookingMenu
    {
        public void BookingMenuChoice()
        {
            int run = 1;
            while (run == 1)
            {
                Console.WriteLine(@"
$$$$$$$\            $$\                 $$\                     
$$  __$$\           $$ |                \__|                    
$$ |  $$ | $$$$$$\  $$ |  $$\ $$$$$$$\  $$\ $$$$$$$\   $$$$$$\  
$$$$$$$\ |$$  __$$\ $$ | $$  |$$  __$$\ $$ |$$  __$$\ $$  __$$\ 
$$  __$$\ $$ /  $$ |$$$$$$  / $$ |  $$ |$$ |$$ |  $$ |$$ /  $$ |
$$ |  $$ |$$ |  $$ |$$  _$$<  $$ |  $$ |$$ |$$ |  $$ |$$ |  $$ |
$$$$$$$  |\$$$$$$  |$$ | \$$\ $$ |  $$ |$$ |$$ |  $$ |\$$$$$$$ |
\_______/  \______/ \__|  \__|\__|  \__|\__|\__|  \__| \____$$ |
                                                      $$\   $$ |
                                                      \$$$$$$  |
                                                       \______/ 
                                                                                        
");
                Console.WriteLine("\n===================================================================================================================\n");
                Console.WriteLine("1. Skapa bokning\n2. Visa bokning\n3. Uppdatera information om bokning\n4. Avsluta bokning\n0. Gå tillbaka ett steg");

                var choice = Console.ReadLine();
                var options = new DbContextOptionsBuilder<ApplicationDbContext>();
                Console.Clear();

                options.UseSqlServer("Server=localhost;Database=AbdiHotel;Trusted_Connection=True;TrustServerCertificate=true;");
                using (var dbContext = new ApplicationDbContext(options.Options))
                {
                    switch (choice)
                    {
                        case "1":
                            var createBooking = new CreateBooking(dbContext);
                            createBooking.NewBooking();
                            break;

                        case "2":
                            var showBooking = new ShowBooking(dbContext);
                            showBooking.DisplayBooking();
                            break;

                        case "3":
                            var updateBooking = new UpdateBooking(dbContext);
                            updateBooking.Update();
                            break;

                        case "4":
                            var deleteBooking = new DeleteBooking(dbContext);
                            deleteBooking.Delete();
                            break;

                        case "0":
                            var rec = new Reception();
                            rec.ReceptionMenu();
                            break;

                        default:
                            Console.WriteLine("Välj ett av alternativen");
                            break;
                    }
                }

            }

        }
    }
}
