namespace CybontrolX.DBModels
{
    public class Client
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }

        public ICollection<Session> Sessions { get; set; } = new List<Session>(); // Один ко многим
        public ICollection<Payment> Payments { get; set; } = new List<Payment>(); // Один ко многим
        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}
