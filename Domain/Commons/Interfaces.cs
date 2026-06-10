using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Commons
{
    public interface ISoftDeletableEntity
    {
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
    }

    public interface IAuditableEntity
    {
        public int? CreateBy { get; set; }
        public int? UpdateBy { get; set; }
    }
}
