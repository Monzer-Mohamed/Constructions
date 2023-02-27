using System;

namespace UserAccount.Entities
{
    public partial class UserAccount
    {

        public int Id { get; set; } 
        public string FullNameAr { get; set; }
        public string FullNameEn { get; set; }
        public string Role { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
