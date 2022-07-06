using RedSocial.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSocial.Core.Domain.Entities
{
    public class Publications : AuditableBaseEntity
    {
        public string Title { get; set; }
        public string UrlPhoto { get; set; }
        public int UserId { get; set; }
        public User Owner { get; set; }
        public ICollection<Comments> Coments { get; set; }
    }
}
