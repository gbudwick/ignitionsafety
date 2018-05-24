using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IS.Data.Model;

namespace IS.Data.Interfaces
{
    public interface IAccountRepository
    {
        string AddAccount(Account account);
    }
}
