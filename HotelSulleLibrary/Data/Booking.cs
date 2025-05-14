using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  HotelSulleLibrary.Data
{
    public class Booking
    {
       [Key]
        public int BookingId { get; set; }
        public int GuestId { get; set; }
        public  int RoomId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public double Price { get; set; }
        public bool IsPaid { get; set; }
        public bool IsValid { get; set; }
        public Guest Guest { get; set; }
        public Room Room { get; set; }

    }
}
