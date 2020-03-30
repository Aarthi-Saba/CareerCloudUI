using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CoreUI.Models
{
    public class CompanyJobSkill
    {
        public Guid Id { get; set; }
        public Guid Job { get; set; }
        public string Skill { get; set; }
        [Display(Name="Skill Level")]
        public string SkillLevel{ get; set; }
        public Int32 Importance { get; set; }
    }
}
