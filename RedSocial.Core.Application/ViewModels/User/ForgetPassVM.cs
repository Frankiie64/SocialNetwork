using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSocial.Core.Application.ViewModels.User
{
    public class ForgetPassVM
    {
        public string Username { get; set; }
        public bool validateUser { get; set; } = false;
    }
}
