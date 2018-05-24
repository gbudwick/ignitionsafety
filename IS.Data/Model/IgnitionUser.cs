using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace IS.Data.Model
{
    public class IgnitionUser : IdentityUser
    {
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public DateTime? DisabledOn { get; set; }

        public bool? IsPropertyOwner { get; set; }
        public bool? IsRenter { get; set; }

        public bool? AccountIsLocked { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string AccountId { get; set; }
        public int Pin { get; set; }
    }
}
