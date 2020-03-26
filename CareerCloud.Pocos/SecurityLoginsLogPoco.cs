using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CareerCloud.Pocos
{
    [Table("Security_Logins_Log")]
    public class SecurityLoginsLogPoco:IPoco
    {
        [Key]
        public Guid Id { get; set; }
        public Guid Login { get; set; }
        [Column("Source_IP")]
        public string SourceIP { get; set; }
        [Column("Logon_Date")]
        public DateTime LogonDate { get; set; }
        [Column("Is_Succesful")]
        public Boolean IsSuccesful { get; set; }
        public virtual SecurityLoginPoco SecurityLogin { get; set; }

    }
}
