using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CareerCloud.Pocos
{
    [Table("Company_Job_Skills")]
    public class CompanyJobSkillPoco :IPoco
    {
        [Key]
        public Guid Id { get; set; }
        public Guid Job { get; set; }
        public string Skill { get; set; }
        [Column("Skill_Level")]
        public string SkillLevel{ get; set; }
        public Int32 Importance { get; set; }
        [NotMapped]
        [Column("Time_Stamp")]
        public byte[] TimeStamp { get; set; }
        public virtual CompanyJobPoco CompanyJob { get; set; }

    }
}
