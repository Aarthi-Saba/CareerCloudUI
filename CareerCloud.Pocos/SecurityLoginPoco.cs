using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CareerCloud.Pocos
{
    [Table("Security_Logins")]
    public class SecurityLoginPoco :IPoco
    {
        [Key]
        public Guid Id { get; set; }
        public string Login { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Date)]
        [Column("Created_Date")]
        public DateTime Created{ get; set; }
        [DataType(DataType.Date)]
        [Column("Password_Update_Date")]
        public DateTime? PasswordUpdate { get; set; }
        [DataType(DataType.Date)]
        [Column("Agreement_Accepted_Date")]
        public DateTime? AgreementAccepted { get; set; }
        [Column("Is_Locked")]
        public Boolean IsLocked { get; set; }
        [Column("Is_Inactive")]
        public Boolean IsInactive { get; set; }
        [Column("Email_Address")]
        public string EmailAddress { get; set; }
        [Column("Phone_Number")]
        public string? PhoneNumber { get; set; }
        [Column("Full_Name")]
        [MaxLength(70)]
        public string FullName { get; set; }
        [Column("Force_Change_Password")]
        public Boolean ForceChangePassword { get; set; }
        [Column("Prefferred_Language")]
        public string? PrefferredLanguage { get; set; }
        [Column("Time_Stamp")]
        [NotMapped]
        public Byte[] TimeStamp { get; set; }
        public virtual ICollection<SecurityLoginsLogPoco> SecurityLoginsLog { get; set; }
        public virtual SecurityLoginsRolePoco SecurityLoginsRole { get; set; }
        public virtual ApplicantProfilePoco ApplicantProfile { get; set; }

    }
}
