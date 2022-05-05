using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace recrutement.Models
{
    public class JobsAppViewModels
    {
        public string JobTitle { get; set; }
        public IEnumerable<JobApplicationModels> Items { get; set; }
    }
}