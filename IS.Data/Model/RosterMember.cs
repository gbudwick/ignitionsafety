using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IS.Data.Model
{
    public class RosterMember : BaseEntity
    {
        [Key]
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string  LastName { get; set; }
        public string ContactPhone { get; set; }
        public Account Account { get; set; }
        public string AccountId { get; set; }
        public Department Department { get; set; }
        public string DepartmentId { get; set; }
    }
}
