using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CoreUI.Models
{
    public class CompanyJobDescriptionModel
    {
        public Guid Id { get; set; }
        public Guid Job{ get; set; }
        [Display(Name="Job Name")]
        public string JobName { get; set; }
        [Display(Name="Job Descriptions")]
        public string JobDescriptions { get; set; }
    }
}
