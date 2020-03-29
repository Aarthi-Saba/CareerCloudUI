using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CoreUI.Models
{
    public class ApplicantProfile
    {
        public Guid Id { get; set; }
        public Guid Login { get; set; }
        [Display(Name = "Current Salary")]
        public decimal? CurrentSalary { get; set; }
        [Display(Name = "Current Rate")]
        public decimal? CurrentRate { get; set; }
        public string Currency { get; set; }
        [Required]
        [Display(Name = "Country Code")]
        public string Country{ get; set; }
        [Required]
        [Column("State_Province_Code")]
        public string Province { get; set; }
        [Required]
        [Column("Street_Address")]
        public string Street { get; set; }
        [Required]
        [Column("City_Town")]
        public string City { get; set; }
        [Required]
        [Column("Zip_Postal_Code")]
        public string PostalCode { get; set; }
    }
}
