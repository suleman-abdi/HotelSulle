using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace  HotelSulleLibrary.Data
{
    public class Guest
    {
        [Key]
        public int GuestId { get; set; }
        public string GuestFirstName { get; set; }
        public string GuestLastName { get; set; }
        public string GuestEmail { get; set; }
        public string Address { get; set; }
        public bool IsActive { get; set; }
        public ICollection<Booking> Bookings { get; set; }


    }
}
