namespace CybontrolX.DBModels
{
    public class Session
    {
        public int Id { get; set; }
        public DateTime SessionStartTime { get; set; }
        public DateTime? SessionEndTime { get; set; }
        public bool IsActive { get; set; }

        public int ClientId { get; set; }
        public int ComputerId { get; set; }
        public int TariffId { get; set; }

        public Client Client { get; set; }
        public Computer Computer { get; set; }
        public Tariff Tariff { get; set; }
    }
}
