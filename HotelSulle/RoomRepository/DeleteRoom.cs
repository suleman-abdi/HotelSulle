using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelSulleLibrary.Data;
using HotelSulleLibrary.GuestRepository;

namespace HotelSulle.RoomRepository
{
    public class DeleteRoom
    {
        private readonly ApplicationDbContext _dbContext;
        public DeleteRoom(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Delete()
        {
            Console.WriteLine("===========================================================================");
            Console.WriteLine("\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t");
            Console.WriteLine("\t1. Inaktivera rum");
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
                        var emptyRoom = _dbContext.Room.Where(g => g.IsAvailable == true).ToList();

                        foreach (var room in emptyRoom)
                        {
                            Console.WriteLine($"\nID: {room.RoomId}\nRumsnummer: {room.RoomNumber}\nTyp av rum: {room.TypeOfRoom}\n");

                        }

                        Console.WriteLine("\nVälj Id på det rum som du vill inaktivera\n");


                        var roomIdToDelete = 0;
                        bool validInput = false;

                        while (!validInput)
                        {
                            string userInput = Console.ReadLine();

                            if (int.TryParse(userInput, out roomIdToDelete))
                            {
                                var roomToDelete = _dbContext.Room.FirstOrDefault(r => r.RoomId == roomIdToDelete);

                                if (roomToDelete != null)
                                {
                                    roomToDelete.IsAvailable = false;
                                    _dbContext.SaveChanges();

                                    Console.WriteLine("Rummet är inaktiverat!");
                                    Console.WriteLine("Tryck på O för att gå tillbaka till huvudmenyn");
                                    Console.ReadLine();

                                    Console.Clear();
                                    var recep = new Reception();
                                    recep.ReceptionMenu();
                                    validInput = true;
                                }
                                else
                                {
                                    Console.WriteLine("Rummet med det angivna ID:et kunde inte hittas. Ange ett annat ID:");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Inmatningen är ogiltig. Vänligen ange ett nummer:");
                            }
                        }


                        _dbContext.SaveChanges();

                        Console.WriteLine("Rummet är inaktiverat!");
                        Console.WriteLine("Tryck på O för att gå tillbaka till huvudmenyn");
                        Console.ReadLine();

                        Console.Clear();
                        var rec = new Reception();
                        rec.ReceptionMenu();
                        break;

                    case "0":
                        Console.Clear();
                        var reception = new Reception();
                        reception.ReceptionMenu();
                        break;

                    default:
                        Console.WriteLine("Fel val! Välj igen");
                        break;
                }
            }
        }
    }
}
