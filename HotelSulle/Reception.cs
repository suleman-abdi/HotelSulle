using HotelSulleLibrary.Data;
using HotelSulle.GuestRepository;
using HotelSulleLibrary;
using HotelSulle.BookingRepository;
using HotelSulle.RoomRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace HotelSulle
{
    public class Reception
    {

        public void ReceptionMenu()
        {
            bool run = true;
            while (run)
            {

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(@"

     ██╗  ██╗ ██████╗ ████████╗███████╗██╗         ███████╗██╗     ███╗   ███╗██╗
     ██║  ██║██╔═══██╗╚══██╔══╝██╔════╝██║         ██╔════╝██║     ████╗ ████║██║
     ███████║██║   ██║   ██║   █████╗  ██║         █████╗  ██║     ██╔████╔██║██║
     ██╔══██║██║   ██║   ██║   ██╔══╝  ██║         ██╔══╝  ██║     ██║╚██╔╝██║██║
     ██║  ██║╚██████╔╝   ██║   ███████╗███████╗    ███████╗███████╗██║ ╚═╝ ██║██║
     ╚═╝  ╚═╝ ╚═════╝    ╚═╝   ╚══════╝╚══════╝    ╚══════╝╚══════╝╚═╝     ╚═╝╚═╝
                                                                                
 
                         
");
                Console.ResetColor();

                Console.WriteLine(" 1. Gäst\n 2. Rum\n 3. Bokning");


                string c = Console.ReadLine();
                Console.Clear();
                switch (c)
                {
                    case "1":
                        var guest = new GuestMenu();
                        guest.GuestMenuChoice();
                        break;

                    case "2":
                        var room = new RoomMenu();
                        room.RoomMenuChoice();
                        break;

                    case "3":
                        var booking = new BookingMenu();
                        booking.BookingMenuChoice();
                        break;

                    default:
                        Console.WriteLine("\nFel inmatning! Vänligen välj ett av alternativen.\n");
                        break;

                }

            }

        }
    }
}
