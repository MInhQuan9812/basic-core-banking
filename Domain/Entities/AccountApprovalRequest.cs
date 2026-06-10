using Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class AccountApprovalRequest : BaseEntity
    {
        public int AccountId { get; set; }
        public Account Account { get; set; } = null!;
        public int UserId { get; set; }
        public User User { get; set; } = null!;
        public string IdDocumentUrl { get; set; } = null!;
        public bool IsApproved { get; set; }
        public DateTime? ProcessedAt { get; set; }
        public int? ProcessedByAdminId { get; set; }
        public string? AdminRemarks { get; set; }
    }
}
