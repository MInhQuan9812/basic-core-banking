using Domain.Commons;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class IdempotencyRecord : BaseEntity
    {
        [MaxLength(100)]
        public required string Key { get; set; }
        public int UserId { get; set; }
        public required string Path { get; set; }
        public required string Method { get; set; }
        public int ResponseStatusCode { get; set; }
        public string? ResponseBody { get; set; }
    }
}
