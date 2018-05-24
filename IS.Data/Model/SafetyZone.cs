using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IS.Data.Model
{
    public class SafetyZone : BaseEntity
    {
        [Key]
        public string Id { get; set; }
        public Account Account { get; set;  }
        public string  AccountId { get; set; }
        public string Name { get; set; }

    }
}
