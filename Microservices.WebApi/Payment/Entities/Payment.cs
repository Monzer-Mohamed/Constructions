using System;

namespace Payment.Entities
{
    public partial class Payment
    {
        public int Id { get; set; }
        public string CardNumber { get; set; }
        public string CardExpiry { get; set; }
        public decimal Amount { get; set; }
        public int ApplicaitonId { get; set; }
        public int TransactionId { get; set; }
        public bool IsPayed { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
