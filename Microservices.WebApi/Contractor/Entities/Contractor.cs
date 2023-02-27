using System;

namespace Contractor.Entities
{
    public partial class Contractor
    {
        public int Id { get; set; }
        public string FullNameAr { get; set; }
        public string FullNameEn { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
