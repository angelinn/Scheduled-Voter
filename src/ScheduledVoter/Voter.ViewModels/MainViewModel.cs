using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using Voter.ViewModels.Services;

namespace Voter.ViewModels
{

    public class MainViewModel : ViewModelBase
    {
        public ICommand ReturnUpCommand { get; private set; }
        private ConfigurationService configurationService;
        private HttpClient client = new HttpClient();

        public MainViewModel()
        {
            ReturnUpCommand = new RelayCommand(async () => await LoginAsync());

            configurationService = SimpleIoc.Default.GetInstance<ConfigurationService>();
        }
        
        private async Task LoginAsync()
        {
            string url = configurationService.Configuration.Url;

            Dictionary<string, string> formEncoded = new Dictionary<string, string>()
            {
                {"username", username },
                {"password", password }
            };
            
            await client.PostAsync(url, new FormUrlEncodedContent(formEncoded));
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
