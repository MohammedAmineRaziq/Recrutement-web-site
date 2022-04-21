using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace recrutement.Models
{
    public class JobModel
    {
        public int Id { get; set; }
        [DisplayName("Le nom de Travaille")]
        public string JobTitle { get; set; }
        [DisplayName("Description")]
        [AllowHtml]
        public string JobContent { get; set; }
        [DisplayName("Image")]
        public string JobImage { get; set; }

        [DisplayName("Domaine")]
        public int CategoryId { get; set; }

        public CategoryModels Category { get; set; }

    }
}