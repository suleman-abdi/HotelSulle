using HotelSulleLibrary.Data;

namespace HotelSulle
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var app = new Application();
            app.Run();

            using (var dbContext = new ApplicationDbContext())
            {
                var rec = new Reception();
                rec.ReceptionMenu();
            }
        }
    }
}
