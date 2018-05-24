using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IS.Services.Interfaces
{
    public interface IUploadService
    {
        void SaveRosterLine(string line, string accountId);
        void ClearRoster(string accountId);
    }
}
