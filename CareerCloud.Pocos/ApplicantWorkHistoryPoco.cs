using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CareerCloud.Pocos
{
    [Table("Applicant_Work_History")]
    public class ApplicantWorkHistoryPoco :IPoco
    {
        [Key]
        public Guid Id { get; set; }
        public Guid Applicant { get; set; }
        [Column("Company_Name")]
        public string CompanyName { get; set; }
        [Column("Country_Code")]
        public string CountryCode { get; set; }
        public string Location { get; set; }
        [Column("Job_Title")]
        public string JobTitle { get; set; }
        [Column("Job_Description")]
        public string JobDescription { get; set; }
        [Column("Start_Month")]
        public Int16 StartMonth { get; set; }
        [Column("Start_Year")]
        public Int32 StartYear { get; set; }
        [Column("End_Month")]
        public Int16 EndMonth { get; set; }
        [Column("End_Year")]
        public Int32 EndYear { get; set; }
        [Column("Time_Stamp")]
        [NotMapped]
        public Byte[] TimeStamp { get; set; }

        public virtual ApplicantProfilePoco ApplicantProfile { get; set; }
        public virtual SystemCountryCodePoco SystemCountryCode { get; set; }
     }
}
