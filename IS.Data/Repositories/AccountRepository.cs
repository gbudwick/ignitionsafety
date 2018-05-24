using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IS.Data.DbContexts;
using IS.Data.Interfaces;
using IS.Data.Model;
using Microsoft.Extensions.Logging;

namespace IS.Data.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private IsDbContext _context;
        private ILogger<AccountRepository> _logger;

        public AccountRepository(ILogger<AccountRepository> logger, IsDbContext context)
        {
            _context = context;
            _logger = logger;
        }



        public string AddAccount(Account account)
        {
            try
            {
                _context.Accounts.Add(account);
                _context.SaveChanges();
                return account.Id;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
    }
}
