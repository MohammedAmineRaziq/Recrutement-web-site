using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace recrutement.Models
{
    public class JobApplicationModels
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime ApplyDate { get; set; }
        public int JobId { get; set; }
        public string UserId { get; set; }

        public virtual JobModel job { get; set; }
    }
}