namespace CybontrolX.DBModels
{
    public class Payment
    {
        public int Id { get; set; }
        public string PaymentId { get; set; } // Уникальный идентификатор платежа от ЮMoney
        public decimal Amount { get; set; } // Сумма платежа
        public string Currency { get; set; }
        public string Status { get; set; } // Статус платежа (Pending, Success, Failed)
        public string Description { get; set; } // Описание платежа
        public DateTime PaymentDateTime { get; set; } // Дата и время создания платежа
        public DateTime? ConfirmedDateTime { get; set; } // Дата и время подтверждения платежа
        public string PaymentMethod { get; set; } // Способ оплаты, например, "ЮMoney"
        public string RedirectUrl { get; set; } // Ссылка на редирект для завершения оплаты
        public string NotificationUrl { get; set; } // URL для уведомлений от ЮMoney
        public string FailureReason { get; set; } // Причина отказа, если платеж не прошел

        public Client Client { get; set; }
        public Employee Employee { get; set; }
    }
}
