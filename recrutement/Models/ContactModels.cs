using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace recrutement.Models
{
    public class ContactModels
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [AllowHtml]
        public string Subject { get; set; }
        public string Message { get; set; }
    }
}