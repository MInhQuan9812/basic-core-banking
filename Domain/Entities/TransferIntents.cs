using Domain.Commons;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class TransferIntents : BaseEntity
    {
        public string TransferIntentId { get; set; } = default!;
        public int FromAccountId { get; set; }
        public int ToAccountId { get; set; }
        public decimal? Amount { get; set; }
        public TransferIntentStatus transferIntentStatus { get; set; } = TransferIntentStatus.Pending;
        public DateTime? CompleAt { get; set; }
        public virtual Account? FromAccount { get; set; }
        public virtual Account? ToAccount { get; set; }
    }
}
