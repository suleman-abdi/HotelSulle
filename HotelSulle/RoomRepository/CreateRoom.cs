using HotelSulleLibrary.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using HotelSulleLibrary.GuestRepository;
using System.Net;

namespace HotelSulle.RoomRepository
{

    public class CreateRoom
    {
        private readonly ApplicationDbContext _dbContext;


        public CreateRoom(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void CreateNewRoom()
        {

            var room = new Room();


            Console.WriteLine("===========================================================================");
            Console.WriteLine("\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t");
            Console.WriteLine("\t1. Lägg till ett rum");
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

                        if (_dbContext.Room.Count() > 15)

                        {
                            Console.WriteLine("Hotellet har nått sin maximala gräns på rum!");
                            Console.ReadLine();
                            Console.Clear();
                            return;
                        }
                        int roomNumber = 0;

                        int quit = 0;

                        while (quit == 0)
                        {
                            Console.Write("\nAnge rumsnummer: ");

                            if (!int.TryParse(Console.ReadLine(), out roomNumber))
                            {
                                Console.WriteLine("Fel inmatning! Ange ett giltigt rumsnummer.");
                            }
                            room = _dbContext.Room.FirstOrDefault(r => r.RoomNumber == roomNumber);
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

                        string typeOfRoom;
                        while (true)
                        {
                            Console.Write("\nAnge rumstyp (enkelrum/dubbelrum): ");
                            typeOfRoom = Console.ReadLine().ToLower();

                            if (string.IsNullOrWhiteSpace(typeOfRoom))
                            {
                                Console.WriteLine("Ogiltigt, försök igen. Alla fält måste fyllas i.");
                            }

                            if (typeOfRoom.ToLower().Equals("enkelrum") || typeOfRoom.ToLower().Equals("dubbelrum"))
                            {
                                break;
                            }

                        }


                        room.RoomNumber = roomNumber;
                        room.TypeOfRoom = typeOfRoom;
                        room.IsAvailable = true;




                        Console.WriteLine("\nAnge pris/kväll: ");
                        int priceForRoom = 0;

                        if (priceForRoom == null)
                        {
                            Console.WriteLine("Priset kan inte vara 0. Mata in ett pris igen");
                        }

                        while (!int.TryParse(Console.ReadLine(), out priceForRoom))
                        {
                            Console.WriteLine("Fel inmatning! Mata in igen");

                        }
                        room.PricePerNight = priceForRoom;

                        if (typeOfRoom.ToLower() == "dubbelrum")
                        {
                            Console.WriteLine("\nTryck 1 för extra sängar eller 2 för att gå vidare.");
                            string val = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(val))
                            {
                                Console.WriteLine("Välj rätt alternativ!");
                            }

                            if (val != "1" || val != "2")
                            {
                                Console.WriteLine("Välj antingen 1 eller 2!");
                                Console.ReadLine();
                            }
                            if (val == "1")
                            {
                                int extraBeds = 0;

                                while (true)
                                {
                                    Console.WriteLine("Ange antal extra sängar (1 eller 2): ");

                                    if (!int.TryParse(Console.ReadLine(), out extraBeds))
                                    {
                                        if (extraBeds == 1 || extraBeds == 2)
                                        {
                                            break;
                                        }

                                        else
                                        {
                                            Console.WriteLine("Välj antingen 1 eller 2!");

                                        }
                                    }

                                    room.ExtraBeds = extraBeds;

                                    _dbContext.Room.Add(room);
                                    _dbContext.SaveChanges();

                                    Console.WriteLine("\nRummet är tillagd!");
                                    Console.ReadLine();

                                    Console.Clear();
                                    var recep = new Reception();
                                    recep.ReceptionMenu();
                                }


                            }

                            else if (val == "2")
                            {
                                _dbContext.Room.Add(room);
                                _dbContext.SaveChanges();

                                Console.WriteLine("\nRummet är tillagd!");
                                Console.ReadLine();

                                Console.Clear();
                                var rec = new Reception();
                                rec.ReceptionMenu();
                            }
                        }

                        _dbContext.Room.Add(room);
                        _dbContext.SaveChanges();

                        Console.WriteLine("\nRummet är tillagd!");
                        Console.WriteLine("Tryck på O för att gå tillbaka till huvudmenyn");
                        Console.ReadLine();


                        Console.Clear();
                        var recept = new Reception();
                        recept.ReceptionMenu();



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
