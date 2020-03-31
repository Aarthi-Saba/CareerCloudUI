using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CoreUI.Models
{
    public class SecurityLogin
    {
        public Guid Id { get; set; }
        public string Login { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Date)]
        public DateTime Created { get; set; }
        [DataType(DataType.Date)]
        public DateTime? PasswordUpdate { get; set; }
        [DataType(DataType.Date)]
        public DateTime? AgreementAccepted { get; set; }

        public Boolean IsLocked { get; set; }

        public Boolean IsInactive { get; set; }
        [Display(Name = "Email")]
        public string EmailAddress { get; set; }
#nullable enable
        public string? PhoneNumber { get; set; }
        [Display(Name ="Full Name")]
        [MaxLength(70)]
        public string FullName { get; set; }

        public Boolean ForceChangePassword { get; set; }
        public string? PrefferredLanguage { get; set; }
    }
}
