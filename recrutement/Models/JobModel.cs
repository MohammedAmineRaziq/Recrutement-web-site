using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

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

        public String UserId { get; set; }


        public virtual CategoryModels Category { get; set; }
        public virtual ApplicationUser User { get; set; }

    }
}