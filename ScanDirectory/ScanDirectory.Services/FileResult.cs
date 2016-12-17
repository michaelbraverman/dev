using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScanDirectory.Services
{
    public enum FileResultStatus
    {
        Unknown,
        Failed,
        Succeeded
    }
    public class FileResult
    {
        public List<string> Files { get; set; }
        public FileResultStatus Status { get; set; }
        public string Message { get; set; }

    }
}
