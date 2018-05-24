using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using IS.Data.Interfaces;
using IS.Data.Model;
using IS.Services.Interfaces;
using IS.Web.Components.ViewModels;
using Microsoft.Extensions.Logging;

namespace IS.Services.Services
{
    public class AccountService : IAccountService
    {
        #region Members

        private IAccountRepository _accountRepository;
        private ILogger<AccountService> _logger;

        #endregion

        public AccountService(IAccountRepository accountRepository, ILogger<AccountService> logger)
        {
            _accountRepository = accountRepository;
            _logger = logger;
        }


        public string RegisterNewAccount(RegisterViewModel model)
        {
            try
            {
                var account = Mapper.Map<Account>(model);
                return _accountRepository.AddAccount(account);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
    }
}
