using Domain.Commons;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Account : BaseEntity
    {
        public required string AccountNumber { get; set; }
        public required AccountType AccountType { get; set; }
        public decimal Balance { get; set; } = 0;
        public int UserId { get; set; }
        public AccountStatus AccountStatus { get; set; } = AccountStatus.Pending;
        public AccountLevel AccountLevel { get; set; } = AccountLevel.Basic;
        public byte[] RowVersion { get; set; } = Guid.NewGuid().ToByteArray();
        public virtual User User { get; set; } = null!;
        //public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}
