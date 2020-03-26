using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CareerCloud.Pocos
{
    [Table("Applicant_Profiles")]
    public class ApplicantProfilePoco : IPoco
    {
        [Key]
        public Guid Id { get; set; }
        public Guid Login { get; set; }
        [Column("Current_Salary")]
        public decimal? CurrentSalary { get; set; }
        [Column("Current_Rate")]
        public decimal? CurrentRate { get; set; }
        public string Currency { get; set; }
        [Column("Country_Code")]
        public string Country{ get; set; }
        [Column("State_Province_Code")]
        public string Province { get; set; }
        [Column("Street_Address")]
        public string Street { get; set; }
        [Column("City_Town")]
        public string City { get; set; }
        [Column("Zip_Postal_Code")]
        public string PostalCode { get; set; }
        [Column("Time_Stamp")]
        [NotMapped]
        public Byte[] TimeStamp { get; set; }
        public virtual ICollection<ApplicantJobApplicationPoco> ApplicantJob { get; set; }
        public virtual ICollection<ApplicantEducationPoco> ApplicantEducation { get; set; }
        public virtual SecurityLoginPoco SecurityLogin { get; set; }
        public virtual SystemCountryCodePoco SystemCountry { get; set; }
        public virtual ICollection<ApplicantResumePoco> ApplicantResume { get; set; }
        public virtual ICollection<ApplicantSkillPoco> ApplicantSkill { get; set; }
        public virtual ICollection<ApplicantWorkHistoryPoco> ApplicantWork { get; set; }

    }
}
