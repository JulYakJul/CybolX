namespace CybontrolX.DBModels
{
    public class Tariff
    {
        public int Id { get; set; } // PK
        public string TariffName { get; set; }
        public TimeSpan SessionTime { get; set; }
        public double Price { get; set; }

        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>(); // Многие к одному
        public ICollection<Session> Sessions { get; set; } = new List<Session>();
    }
}
