using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScanDirectory.Services
{
    public class FilesService
    {
        public Task<FileResult> GetFiles(string path)
        {
            return Task.Run(() =>
             {
                 FileResult result = new FileResult { Files = new List<string>() };
                 try
                 {
                     DirectoryInfo directory = new DirectoryInfo(path);
                     var subDirectories = directory.GetDirectories()
                               .Where(d => (d.Attributes & FileAttributes.ReparsePoint) == 0
                                      && (d.Attributes & FileAttributes.System) == 0);

                     foreach (var d in subDirectories)
                     {
                         var files = SecuredGetFiles(d.FullName);
                         if (files != null)
                             result.Files.AddRange(files);
                     }

                     result.Status = FileResultStatus.Succeeded;
                     result.Message = "Files have been found";
                     return result;
                 }
                 catch (Exception ex)
                 {
                     result.Message = $"Files have not been found. The possible reason is {ex.ToString()}";
                     result.Status = FileResultStatus.Failed;
                     return result;
                 }

             });

        }

        //TODO: the method is not effective. in the case of access denied to the file it throws exception and reduce performane dramatically
        private string[] SecuredGetFiles(string path)
        {
            try
            {
                return Directory.GetFiles(path, "*.*", SearchOption.AllDirectories).Where(w => ConvertBytesToMegabytes(new FileInfo(w).Length) >= 100).ToArray();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private double ConvertBytesToMegabytes(long bytes)
        {
            return (bytes / 1024f) / 1024f;
        }

        private double ConvertKilobytesToMegabytes(long kilobytes)
        {
            return kilobytes / 1024f;
        }
    }
}
