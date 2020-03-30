using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CoreUI.Models
{
    public class CompanyDescriptionModel
    {
        public Guid Id { get; set; }
        public Guid Company { get; set; }
        [Display(Name = "Language")]
        public string LanguageId { get; set; }
        [Display(Name="Company Name")]
        public string CompanyName { get; set; }
        [Display(Name = "Company Description")]
        public string CompanyDescription { get; set; }
    }
}
