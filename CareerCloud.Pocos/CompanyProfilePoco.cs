using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CareerCloud.Pocos
{
    [Table("Company_Profiles")]
    public class CompanyProfilePoco : IPoco
    {
        [Key]
        public Guid Id { get; set; }
        [Column("Registration_Date")]
        public DateTime RegistrationDate { get; set; }
        [Column("Company_Website")]
        public string? CompanyWebsite { get; set; }
        [Column("Contact_Phone")]
        public string ContactPhone { get; set; }
        [Column("Contact_Name")]
        public string? ContactName { get; set; }
        [Column("Company_Logo")]
        public Byte[]? CompanyLogo { get; set; }
        [NotMapped]
        [Column("Time_Stamp")]
        public Byte[]? TimeStamp { get; set; }

        public virtual ICollection<CompanyDescriptionPoco> CompanyDescription { get; set; }
        public virtual ICollection<CompanyJobPoco> CompanyJob { get; set; }
        public virtual ICollection<CompanyLocationPoco> CompanyLocation { get; set; }
    }
}
