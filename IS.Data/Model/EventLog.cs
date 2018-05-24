using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IS.Data.Model
{
    public class EventLog
    {
        public int Id { get; set; }
        public int EventId { get; set; }

        [MaxLength ( 50 )]
        public string LogLevel { get; set; }

        [MaxLength ( 4000 )]
        public string Message { get; set; }

        public DateTime CreatedTime { get; set; }
    }
}
