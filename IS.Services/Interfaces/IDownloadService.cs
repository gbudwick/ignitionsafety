using Microsoft.AspNetCore.Mvc;

namespace IS.Services.Interfaces
{
        public interface IDownloadService
        {
            string DownloadFile(string accountId);
            string GetSampleFileName();
        }
}
