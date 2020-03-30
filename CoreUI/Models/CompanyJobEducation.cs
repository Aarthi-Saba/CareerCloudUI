using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CoreUI.Models
{
    public class CompanyJobEducation
    {
        public Guid Id { get; set; }
        public Guid Job { get; set; }
        public string Major { get; set; }
        public Int16 Importance { get; set; }
    }
}
