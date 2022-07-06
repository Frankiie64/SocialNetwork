using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSocial.Core.Application.ViewModels.Comments
{
    public class SaveCommentVM
    {
        public int Id { get; set; }
        public string comment { get; set; }
        public int PublicationId { get; set; }
        public int UserId { get; set; }
    }
}
