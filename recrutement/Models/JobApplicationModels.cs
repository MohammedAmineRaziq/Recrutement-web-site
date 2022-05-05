using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace recrutement.Models
{
    public class JobApplicationModels
    {
        public int Id { get; set; }
        [DisplayName("Message")]
        public string Message { get; set; }
        public DateTime ApplyDate { get; set; }
        [DisplayName("CV")]
        public string CV { get; set; }
    
        public int JobId { get; set; }
        public string UserId { get; set; }

        public virtual JobModel job { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}