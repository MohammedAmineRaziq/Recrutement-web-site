using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace recrutement.Models
{
    public class CategoryModels
    {   public int Id { get; set; }
        [Required]
        [Display(Name = "Domaine")]
        public string CategoryName { get; set; }
        [Required]
        [Display(Name = "Description")]
        public string CategoryDescription { get; set; }
        public virtual ICollection<JobModel> jobs { get; set; }

    }
}