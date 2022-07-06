using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;


namespace RedSocial.Core.Application.ViewModels.Publication
{
    public class SavePublicationVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo es obligatorio")]
        [DataType(DataType.Text)]
        public string Title { get; set; }
        public string UrlPhoto { get; set; }
        public IFormFile File { get; set; }

        public int UserId { get; set; }
        public DateTime Creadted { get; set; }

    }
}
