using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IS.Data.Model
{
    public class Department  : BaseEntity
    {
        [Key]
        public string Id { get; set; }
        [MaxLength(50, ErrorMessage = "Department name has a maximum of 50 charaters")]
        public string Name { get; set; }
        public Account Account { get; set; }
        public string  AccountId { get; set; }
        public string SafetyZoneId { get; set; }
        public SafetyZone SafetyZone { get; set; }
    }
}
