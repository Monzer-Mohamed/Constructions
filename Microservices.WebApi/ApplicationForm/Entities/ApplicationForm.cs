using System;
using System.Collections.Generic; 

namespace ApplicationForm.Entity
{
    public partial class ApplicationForm
    {
        public int Id { get; set; }
        public int ConstructionId { get; set; }
        public int? ContractorId { get; set; }
        public string Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
