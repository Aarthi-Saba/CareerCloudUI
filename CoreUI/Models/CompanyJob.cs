using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CoreUI.Models
{
    public class CompanyJob
    {
        public Guid Id { get; set; }
        public Guid Company { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name="Profile Created On")]
        public DateTime ProfileCreated { get; set; }
        [Column("Is_Inactive")]
        public Boolean IsInactive { get; set; }
        [Display(Name="Internal Position")]
        [Column("Is_Company_Hidden")]
        public Boolean IsCompanyHidden { get; set; }
    }
}
