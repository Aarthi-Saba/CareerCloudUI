using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CoreUI.Models
{
    public class CompanyProfile
    {
        public Guid Id { get; set; }
        [DataType(DataType.Date)]
        public DateTime RegistrationDate { get; set; }
        [Display(Name = "Company Website")]
#nullable enable
        public string? CompanyWebsite { get; set; }
        [Display(Name="Contact Phone")]
        public string ContactPhone { get; set; }
        [Display(Name = "Contact Name")]
#nullable enable
        public string? ContactName { get; set; }
    }
}
