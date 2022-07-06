using RedSocial.Core.Application.ViewModels.Comments;
using RedSocial.Core.Application.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSocial.Core.Application.ViewModels.Publication
{
    public class PublicationVM
    {
        public int id { get; set; }
        public string Title { get; set; }
        public string UrlPhoto { get; set; }
        public int UserId { get; set; }
        public UserVM Owner { get; set; }
        public DateTime Creadted { get; set; }
        public List<CommentVM> Coments { get; set; }
    }
}
