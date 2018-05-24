using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IS.Web.Components.Exceptions
{
    public class ForeignKeyError : Exception
    {
        public ForeignKeyError(string message) : base(message)
        {
            
        }
    }
}
