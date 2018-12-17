using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using HtmlAgilityPack;
using System.Collections.Generic;
using System.Net.Http;
using System.Security;
using System.Threading.Tasks;
using System.Windows.Input;
using Voter.ViewModels.Services;

namespace Voter.ViewModels
{

    public class MainViewModel : BaseViewModel
    {
        public ICommand ReturnUpCommand { get; private set; }
        private ConfigurationService configurationService;
        private HttpClient client = new HttpClient();
        
        public MainViewModel()
        {
            ReturnUpCommand = new RelayCommand(async () => await LoginAsync());

            configurationService = SimpleIoc.Default.GetInstance<ConfigurationService>();
            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/69.0.3497.100 Safari/537.36 OPR/56.0.3051.116");
        }

        private async Task LoginAsync()
        {
            if (await DoLoginAsync())
                interactionService.ShowMessageBox("Login", "Login successful");
            else
                interactionService.ShowMessageBox("Login", "Login incorrect");
        }
        
        private async Task<bool> DoLoginAsync()
        {
            Dictionary<string, string> formEncoded = new Dictionary<string, string>()
            {
                {"accountName", username },
                {"password", Password.ToString() }
            };

            foreach (KeyValuePair<string, string> hiddenField in await GetHiddenFieldsAsync())
                formEncoded.Add(hiddenField.Key, hiddenField.Value);
            
            return await SendLoginRequestAsync(new FormUrlEncodedContent(formEncoded));
        }

        private async Task<bool> SendLoginRequestAsync(FormUrlEncodedContent content)
        {
            HttpResponseMessage response = await client.PostAsync(configurationService.Configuration.LoginPostFull, content);
            string loginHtml = await response.Content.ReadAsStringAsync();
            if (loginHtml.ToLower().Contains("logging in"))
                return true;

            return false;
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

        public string Password { private get; set; }
    }
}
