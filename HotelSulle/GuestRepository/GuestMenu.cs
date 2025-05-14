using HotelSulleLibrary.Data;
using HotelSulle.GuestRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace HotelSulle.GuestRepository
{
    public class GuestMenu
    {

        public void GuestMenuChoice()
        {
            int run = 1;
            while (run == 1)
            {
                Console.WriteLine(@" 
 $$$$$$\    $\ $\              $$\     
$$  __$$\   \_|\_|             $$ |    
$$ /  \__| $$$$$$\   $$$$$$$\$$$$$$\   
$$ |$$$$\  \____$$\ $$  _____\_$$  _|  
$$ |\_$$ | $$$$$$$ |\$$$$$$\   $$ |    
$$ |  $$ |$$  __$$ | \____$$\  $$ |$$\ 
\$$$$$$  |\$$$$$$$ |$$$$$$$  | \$$$$  |
 \______/  \_______|\_______/   \____/ 
                                       
");
                Console.WriteLine("\n============================================================================================================\n");
                Console.WriteLine("1. Lägg till gäst\n2. Visa gäst\n3. Uppdatera information om gäst\n4. Inaktivera gäst\n0. Gå tillbaka till huvudmenyn");

                var choice = Console.ReadLine();
                var options = new DbContextOptionsBuilder<ApplicationDbContext>();
                Console.Clear();

                options.UseSqlServer("Server=localhost;Database=AbdiHotel;Trusted_Connection=True;TrustServerCertificate=true;");
                using (var dbContext = new ApplicationDbContext(options.Options))
                {
                    switch (choice)
                    {

                        case "1":
                            var createGuest = new CreateGuest(dbContext);
                            createGuest.CreateNewGuest();
                            break;

                        case "2":
                            var showGuest = new ShowGuest(dbContext);
                            showGuest.DisplayGuest();
                            break;

                        case "3":
                            var updateGuest = new UpdateGuest(dbContext);
                            updateGuest.Update();
                            break;

                        case "4":
                            var deleteGuest = new DeleteGuest(dbContext);
                            deleteGuest.Delete();
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
