using System;

namespace Construction.Microservice.Entities
{

    public partial class Construction
    {
        public int Id { get; set; }
        public string OwnerName { get; set; }
        public string Address { get; set; }
        public decimal? Price { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
