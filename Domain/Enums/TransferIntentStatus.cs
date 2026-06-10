using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
    public enum TransferIntentStatus
    {
        Pending = 0,
        Completed = 1,
        Cancelled = 2,
        Failed = 3
    }
}
