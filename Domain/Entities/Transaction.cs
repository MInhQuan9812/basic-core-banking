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
        public required int AccountId { get; set; }//Main account 
        public decimal BalanceAfter { get; set; }//Balance after transaction
        public int? RelatedAccountId { get; set; }//Dest account for transfers
        public string ReferenceId { get; set; } = null!;//External/System reference
        public string? ReferenceNumber { get; set; }//Unique reference for the transaction
        public string? Description { get; set; }
        public virtual Account Account { get; set; } = null!;
        public virtual Account? RelatedAccount { get; set; }
    }
}
