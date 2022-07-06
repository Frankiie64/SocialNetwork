using RedSocial.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSocial.Core.Domain.Entities
{
    public class Comments : AuditableBaseEntity
    {
        public string comment { get; set; }
        public int PublicationId { get; set; }
        public Publications Publication { get; set; }
        public int UserId { get; set; }
        public User Owner { get; set; }
    }
}
