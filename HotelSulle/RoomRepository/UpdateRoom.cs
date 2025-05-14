using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelSulle.BookingRepository;
using HotelSulleLibrary.Data;

namespace HotelSulle.RoomRepository
{
    public class UpdateRoom
    {
        private readonly ApplicationDbContext _dbContext;
        public UpdateRoom(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Update()
        {

            Console.WriteLine("===========================================================================");
            Console.WriteLine("\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t");
            Console.WriteLine("\t1. Uppdatera rum");
            Console.WriteLine("\t0. Huvudmenyn");
            Console.WriteLine("\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t");
            Console.WriteLine("===========================================================================");

            bool run = true;
            while (run)
            {
                string choic = Console.ReadLine();
                switch (choic)
                {
                    case "1":
                        foreach (var room in _dbContext.Room)
                        {
                            Console.WriteLine("\n================================");
                            Console.WriteLine($"Rum Id: {room.RoomId}");
                            Console.WriteLine($"Rumsnummer: {room.RoomNumber}");
                            Console.WriteLine($"Typ av rum: {room.TypeOfRoom}");
                            Console.WriteLine($"Pris/natt: {room.PricePerNight}");
                            Console.WriteLine($"Extra säng: {room.ExtraBeds}");
                            Console.WriteLine("==================================\n");
                        }

                        Console.WriteLine("Välj Id på det rum som du vill uppdatera");
                        int roomId = 0;

                        var roomToUpdate = new Room();

                        while (true)
                        {
                            if (!int.TryParse(Console.ReadLine(), out roomId))
                            {
                                Console.WriteLine("Fel inmatning! Ange ett giltigt nummer.");
                            }
                            roomToUpdate = _dbContext.Room.FirstOrDefault(r => r.RoomId == roomId);

                            if (roomToUpdate != null)
                            {
                                break;
                            }
                            Console.WriteLine("Rummet existerar inte! Ange ett giltigt rum Id");
                        }

                        Console.Clear();


                        int roomNumberUpdate = 0;
                        int quit = 0;

                        while (quit == 0)
                        {
                            Console.WriteLine("Ange nytt rumsnummer: ");
                            if (!int.TryParse(Console.ReadLine(), out roomNumberUpdate))
                            {
                                Console.WriteLine("Fel inmatning! Ange ett giltigt rumsnummer.");
                            }
                            var room = _dbContext.Room.FirstOrDefault(r => r.RoomNumber == roomNumberUpdate);
                            if (room != null)
                            {
                                Console.WriteLine("\nRum nummer finns redan,\n");
                                quit = 0;

                            }
                            else
                            {
                                quit = 1;
                                break;
                            }
                        }

                        Console.WriteLine("Ange ny rumstyp (enkelrum/dubbelrum): ");
                        string typeOfRoomUpdate = Console.ReadLine();

                        if (!typeOfRoomUpdate.ToLower().Equals("enkelrum") && !typeOfRoomUpdate.ToLower().Equals("dubbelrum"))
                        {
                            Console.WriteLine("Det måste vara enkelrum eller dubbelrum! Du kommer nu hänvisas till rum menyn");
                            Console.ReadLine();
                            Console.Clear();
                            return;
                        }


                        Console.WriteLine("Ange pris/natt: ");
                        var pricePerNight = 0;

                        while (!int.TryParse(Console.ReadLine(), out pricePerNight))
                        {
                            Console.WriteLine("Fel inmatning! Du hänvisas nu till rum menyn. Klicka enter");
                            Console.ReadLine();
                            Console.Clear();
                            return;

                        }


                        if (string.IsNullOrWhiteSpace(typeOfRoomUpdate))
                        {
                            Console.WriteLine("Ogiltigt, försök igen. Alla fält måste fyllas i. Du hänvisas nu till rum menyn. Klicka enter");
                            Console.ReadLine();
                            Console.Clear();
                            return;
                        }

                        else
                        {
                            roomToUpdate.RoomNumber = roomNumberUpdate;
                            roomToUpdate.TypeOfRoom = typeOfRoomUpdate;
                            roomToUpdate.PricePerNight = pricePerNight;

                            if (typeOfRoomUpdate.ToLower() == "dubbelrum")
                            {
                                Console.WriteLine("\n1. Lägg till extra säng\n2. Gå vidare\n");
                                string val = Console.ReadLine();
                                switch (val)
                                {
                                    case "1":
                                        Console.WriteLine("Ange antal extra sängar (1 eller 2): ");
                                        int extraBeds = 0;


                                        while (!int.TryParse(Console.ReadLine(), out extraBeds))
                                        {
                                            Console.WriteLine("Fel inmatning!");
                                        }

                                        if (roomToUpdate.ExtraBeds <= 2)
                                        {
                                            Console.WriteLine("Rummet har nått maxgränsen på extra sängar");
                                            Console.ReadLine();
                                            return;
                                        }

                                        roomToUpdate.ExtraBeds = extraBeds;

                                        _dbContext.SaveChanges();

                                        Console.WriteLine("\nRummet är uppdaterad!");
                                        Console.WriteLine("Tryck på O för att gå tillbaka till huvudmenyn");

                                        Console.Clear();
                                        var reception = new Reception();
                                        reception.ReceptionMenu();

                                        break;

                                    case "2":
                                        _dbContext.SaveChanges();

                                        Console.WriteLine("\nRummet är uppdaterad!");
                                        Console.WriteLine("Tryck på O för att gå tillbaka till huvudmenyn");


                                        Console.Clear();
                                        var r = new Reception();
                                        r.ReceptionMenu();

                                        break;
                                }


                            }
                        }

                        _dbContext.SaveChanges();
                        Console.WriteLine("\nRummet är uppdaterad!");
                        Console.ReadLine();

                        Console.Clear();
                        var recep = new Reception();
                        recep.ReceptionMenu();
                        break;
                    case "0":
                        Console.Clear();
                        var rec = new Reception();
                        rec.ReceptionMenu();
                        break;

                    default:
                        Console.WriteLine("Fel val! Välj igen");
                        break;
                }
            }
        }
    }
}
