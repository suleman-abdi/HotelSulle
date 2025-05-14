using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelSulleLibrary.Data;
using HotelSulle.GuestRepository;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace HotelSulle.RoomRepository
{
    public class ShowRoom
    {
        private readonly ApplicationDbContext _dbContext;

        public ShowRoom(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void DisplayRoom()
        {
            Console.WriteLine("===========================================================================");
            Console.WriteLine("\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t");
            Console.WriteLine("\t1. Se lediga rum");
            Console.WriteLine("\t2. Se upptagna rum ");
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
                        var availableRooms = _dbContext.Room.Where(r => r.IsAvailable == true).ToList();
                        foreach (var room in availableRooms)
                        {

                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("\n===============================================");
                            Console.WriteLine($"ID: {room.RoomId}                               ");
                            Console.WriteLine($"Rumsnummer: {room.RoomNumber}                 ");
                            Console.WriteLine($"Rumstyp: {room.TypeOfRoom}                    ");
                            Console.WriteLine($"Extra säng/sängar: {room.ExtraBeds}           ");
                            Console.WriteLine("================================================\n");
                            Console.ResetColor();
                        }

                        Console.WriteLine("Tryck på O för att gå tillbaka till huvudmenyn");

                        break;

                    case "2":

                        var preoccupiedRooms = _dbContext.Room.Where(r => r.IsAvailable == false).ToList();
                        if (preoccupiedRooms.Count > 0)
                        {
                            foreach (var room in preoccupiedRooms)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("===============================================");
                                Console.WriteLine($"ID: {room.RoomId}                             ");
                                Console.WriteLine($"Rumsnummer: {room.RoomNumber}                 ");
                                Console.WriteLine($"Rumstyp: {room.TypeOfRoom}                    ");
                                Console.WriteLine($"Extra säng/sängar: {room.ExtraBeds}           ");
                                Console.WriteLine("================================================");
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
                        var backTo = new RoomMenu();
                        backTo.RoomMenuChoice();
                        run = false;
                        break;

                    default:
                        Console.WriteLine("Fel inmatning! Tryck enter för att gå vidare");

                        break;
                }


            }
        }
    }
}
