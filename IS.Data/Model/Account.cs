using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IS.Data.Model
{
    public class Account : BaseEntity
    {
        [Key]
        public string Id { get; set; }
        public string AccountOwner { get; set; }
        public string CompanyName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public bool AccountLocked { get; set; }
        public DateTime? DateCanceled { get; set; }
        public DateTime? TrialStartDate { get; set; }
        public string PrimaryContactUserId { get; set; }
        public string SecondaryContactUserId { get; set; }
        public DateTime TrialPeriodExpiration { get; set; }
        public bool ReadOnly { get; set; }
    }
}
