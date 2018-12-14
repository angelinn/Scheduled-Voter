using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;

namespace Voter.ViewModels
{

    public class MainViewModel : ViewModelBase
    {
        public ICommand ReturnUpCommand { get; private set; }

        public MainViewModel()
        {
            ReturnUpCommand = new RelayCommand(OnReturnUp);
        }
        
        private void OnReturnUp()
        {
            
        }
        
        private string username;
        public string Username
        {
            get
            {
                return username;
            }
            set
            {
                username = value;
                RaisePropertyChanged();
            }
        }
        
        private string password;
        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
                RaisePropertyChanged();
            }
        }
    }
}
