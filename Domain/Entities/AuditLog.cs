using Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class AuditLog : BaseEntity
    {
        public string EntityName { get; set; } = null!;
        public int EntityId { get; set; }
        public string Action { get; set; } = null!;
        public int? ChangedBy { get; set; }
        public DateTime DateTime { get; set; } = DateTime.UtcNow;
        public string? Changes { get; set; }

    }
}
