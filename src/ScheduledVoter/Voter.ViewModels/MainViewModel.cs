using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using HtmlAgilityPack;
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
            Dictionary<string, string> formEncoded = new Dictionary<string, string>()
            {
                {"username", username },
                {"password", password }
            };

            foreach (KeyValuePair<string, string> hiddenField in await GetHiddenFieldsAsync())
                formEncoded.Add(hiddenField.Key, hiddenField.Value);
            
            await client.PostAsync(configurationService.Configuration.LoginFull, new FormUrlEncodedContent(formEncoded));
        }
        
        private async Task<List<KeyValuePair<string, string>>> GetHiddenFieldsAsync()
        {
            List<KeyValuePair<string, string>> fields = new List<KeyValuePair<string, string>>();
            
            HttpResponseMessage loginResponse = await client.GetAsync(configurationService.Configuration.LoginFull);
            string loginHtml = await loginResponse.Content.ReadAsStringAsync();

            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(loginHtml);

            HtmlNodeCollection hiddenNodes = document.DocumentNode.SelectNodes("//input[@type='hidden']");
            foreach (HtmlNode node in hiddenNodes)
                fields.Add(new KeyValuePair<string, string>(node.Attributes["name"].Value, node.Attributes["value"].Value));

            return fields;
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
