using Domain.Commons;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Transaction : BaseEntity
    {
        public required TransactionType Type { get; set; }
        public required decimal Amount { get; set; }
        public required int AccountId { get; set; }
        public decimal BalanceAfter { get; set; }
        public int? RelatedAccountId { get; set; }
        public string ReferenceId { get; set; } = null!;
        public string? ReferenceNumber { get; set; }
        public string? Description { get; set; }
        public virtual Account Account { get; set; } = null!;
        public virtual Account? RelatedAccount { get; set; }
    }
}
