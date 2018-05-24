using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IS.Web.Components.ViewModels
{
    public class EmailSettingsModel
    {
        public string FromDomain  { get; set; } 
        public string SmtpServer { get; set; }
        public int Port { get; set; }
    }
}
