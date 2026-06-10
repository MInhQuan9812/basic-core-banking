using Domain.Commons;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class User : BaseEntity
    {
        public required string FullName { get; set; }
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public UserRole userRole { get; set; } = UserRole.User;
        public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();

    }
}
