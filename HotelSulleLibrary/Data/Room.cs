using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  HotelSulleLibrary.Data
{
    public class Room
    {
        [Key]
        public int RoomId { get; set; }
        public int RoomNumber { get; set; }
        public string TypeOfRoom { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime BookingStartTime { get; set; }
        public DateTime BookingEndTime { get; set; }
        public int ExtraBeds { get; set; }
        public decimal PricePerNight { get; set; }
        public ICollection<Booking> Bookings { get; set; }

    }
}
