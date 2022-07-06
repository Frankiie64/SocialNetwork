using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSocial.Core.Application.ViewModels.User
{
    public class SaveUserVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo nombre es obligatorio")]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Este campo apellido es obligatorio")]
        [DataType(DataType.Text)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Este campo nombre de ususario es obligatorio")]
        [DataType(DataType.Text)]
        public string Username { get; set; }


        [Required(ErrorMessage = "Este campo es obligatorio")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres.")]
        
        public string Password { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Ambas contraseña deben coincidir")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Numero invalido")]
        [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Debe tener 10 numeros sin guiones o parentesis.")]
        public string Phone { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile File { get; set; }

        public string UrlPhoto { get; set; }
        public bool validateUser { get; set; } = false;
        public bool Active { get; set; } = false;

    }
}
