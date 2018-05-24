using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IS.Data.Interfaces;
using IS.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;

namespace IS.Services.Services
{
    public class DownloadService : IDownloadService
    {
        private IHostingEnvironment _environment
            ;

        private IDownloadRepository _downloadRepository;

        public DownloadService(IHostingEnvironment environment, IDownloadRepository downloadRepository)
        {
            _environment = environment;
            _downloadRepository = downloadRepository;
        }

        public string GetSampleFileName()
        {
           return Path.Combine( _environment.WebRootPath, "downloads", "IgnitionSafety-SampleData.tsv" );
        }


        public string DownloadFile(string accountId)
        {
            var path = Path.Combine( _environment.WebRootPath, "downloads", "IgnitionSafety-" + accountId + ".txt" );

            if ( File.Exists( path ) )
                File.Delete(path);

            var contacts = _downloadRepository.GetDownloadContents(accountId);
            var sb = new StringBuilder();

                // Create a file to write to.
            using (StreamWriter sw = File.CreateText(path))
            {
                sw.WriteLine("FirstName\tLastName\tContactPhone\tDepartment\tSafetyZone");
            
                foreach (var contact in contacts )
                    {
                        sb.Append(contact.FirstName);
                        sb.Append("\t");
                        sb.Append(contact.LastName);
                        sb.Append("\t");
                        sb.Append(contact.ContactPhone);
                        sb.Append("\t");
                        sb.Append(contact.Department);
                        sb.Append("\t");
                        sb.Append(contact.SafetyZone);
                        sw.WriteLine(sb.ToString());
                        sb.Clear();
                    }
                }
            return path;
        }
    }
}
