using ScanDirectory.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScanDirectory.ViewModels
{
    public class FileViewModel : BaseViewModel
    {
        private string _fileName;
        public string FileName
        {
            get { return _fileName; }
            set
            {
                _fileName = value; OnPropertyChanged();
            }
        }
    }
}
