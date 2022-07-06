using RedSocial.Core.Application.ViewModels.Publication;
using RedSocial.Core.Application.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSocial.Core.Application.ViewModels.Comments
{
    public class CommentVM
    {
        public int Id { get; set; }
        public string comment { get; set; }
        public DateTime Creadted { get; set; }
        public int PublicationId { get; set; }
        public int UserId { get; set; }
        public PublicationVM Publication { get; set; }
        public UserVM Owner { get; set; }
    }
}
