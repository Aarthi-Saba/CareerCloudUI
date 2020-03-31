using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CareerCloud.Pocos
{
    [Table("Company_Jobs")]
    public class CompanyJobPoco :IPoco
    {
        [Key]
        public Guid Id { get; set; }
        public Guid Company { get; set; }
        [Column("Profile_Created")]
        [DataType(DataType.Date)]
        public DateTime ProfileCreated { get; set; }
        [Column("Is_Inactive")]
        public Boolean IsInactive { get; set; }
        [Column("Is_Company_Hidden")]
        public Boolean IsCompanyHidden { get; set; }
        [NotMapped]
        [Column("Time_Stamp")]
        public byte[]? TimeStamp { get; set; }

        public virtual ICollection<ApplicantJobApplicationPoco> ApplicantJob { get; set; }
        public virtual ICollection<CompanyJobSkillPoco> CompanyJobSkill { get; set; }
        public virtual CompanyJobEducationPoco CompanyJobEducation { get; set; }
        public virtual CompanyProfilePoco CompanyProfile { get; set; }
        public virtual CompanyJobDescriptionPoco CompanyJobDescription { get; set; }
    }
}
