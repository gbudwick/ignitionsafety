using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IS.Web.Components.ViewModels;

namespace IS.Services.Interfaces
{
    public interface IAccountService
    {
        string RegisterNewAccount(RegisterViewModel model);
    }
}
