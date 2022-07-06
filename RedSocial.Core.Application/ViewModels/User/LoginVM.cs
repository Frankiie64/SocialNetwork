using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSocial.Core.Application.ViewModels.User
{
   public class LoginVM
    {
        [Required(ErrorMessage = "El campo nombre de usuario es obligatorio")]
        [DataType(DataType.Text)]
        public string Username { get; set; }

        [Required(ErrorMessage = "El campo contraseña es obligatorio")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool validate { get; set; } = false;
        public bool Active { get; set; } 

    }
}
