using ScanDirectory.Infra;
using ScanDirectory.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScanDirectory.ViewModels
{
    public class MainViewModel : BaseViewModel
    {

        private FileResultStatus _fileStatus;
        public FileResultStatus FilesStatus
        {
            get { return _fileStatus; }
            set
            {
                _fileStatus = value;
                OnPropertyChanged();
            }
        }

        private string _messageResult;
        public string MessageResult
        {
            get { return _messageResult; }
            set
            {
                _messageResult = value;
                OnPropertyChanged();
            }
        }

        private List<FileViewModel> _fileViewModels { get; set; }
        public List<FileViewModel> FileViewModels
        {
            get { return _fileViewModels; }
            set
            {
                _fileViewModels = value;
                OnPropertyChanged();
            }
        }
        private readonly FilesService _filesService;
        public MainViewModel()
        {
            FileViewModels = new List<FileViewModel>();
            _filesService = new FilesService();
            Init();
        }

        private async void Init()
        {
            IsBusy = true;
            var res = await _filesService.GetFiles(@"c:\windows");
            IsBusy = false;
            FileViewModels = res.Files.Select(s => new FileViewModel { FileName = s }).ToList();
            MessageResult = res.Message;
            FilesStatus = res.Status;

        }
    }
}
