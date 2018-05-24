using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IS.Data.Model
{
    public class MembersStatus : BaseEntity
    {
        public string id { get; set; }
        public string MemberId { get; set; }
        public int Status { get; set; }
        public bool Locked { get; set; }
    }
}
