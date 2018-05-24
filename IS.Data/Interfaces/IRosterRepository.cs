using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IS.Data.Interfaces
{
    public interface IRosterRepository
    {
        void AddEntry(string firstName, string lastName, string contactPhone, string departmentId, string accountId);
        void ClearAllMembersForAccount(string accountId);
    }
}
