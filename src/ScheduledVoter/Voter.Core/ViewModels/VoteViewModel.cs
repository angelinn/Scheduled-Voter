using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Voter.Core.Messages;

namespace Voter.Core.ViewModels
{
    public class VoteViewModel : BaseViewModel
    {
        private const int REQUEST_DELAY_MS = 5000;

        private static Random random = new Random();

        public ICommand LoadedCommand { get; private set; }
        public ICommand VoteCommand { get; private set; }
        public ICommand CheckVoteCommand { get; private set; }
        
        
        public VoteViewModel()
        {
            LoadedCommand = new RelayCommand(async () => await CheckVotesAsync());
            VoteCommand = new RelayCommand(async () => await VoteAsync());
            CheckVoteCommand = new RelayCommand(async () => await CheckVotesAsync());
        }

        public async Task CheckVotesAsync()
        {
            Log("Checking votes...");
            HttpResponseMessage response = await httpClient.GetAsync(GetUrl(Constants.CheckVotesKey));
            string html = await response.Content.ReadAsStringAsync();

            CanVote = html.Contains(configurationService[Constants.CanVoteKey]);
            Log($"Can vote: {canVote}");
        }

        public async Task VoteAsync()
        {
            Log("Voting begins.");
            
            int sleepMs = 0;
            foreach (string voteUrl in configurationService.GetArray(Constants.VotesKey))
            {
                string fullUrl = configurationService[Constants.BaseKey] + voteUrl;
                Log($"Voting at {fullUrl} ...");
                await httpClient.GetAsync(fullUrl);

                sleepMs = REQUEST_DELAY_MS + random.Next(10, 1000);
                Log($"Waiting {sleepMs} ms...");
                await Task.Delay(sleepMs);
            }
            
            Log("Voting completed.");
        }

        private void Log(string message)
        {
            LogBuffer += $"[{DateTime.Now}] {message}\n";
            MessengerInstance.Send(new LogAddedMessage());
        }

        private bool canVote;
        public bool CanVote
        {
            get
            {
                return canVote;
            }
            set
            {
                canVote = value;
                RaisePropertyChanged();
            }
        }

        private string logBuffer;
        public string LogBuffer
        {
            get
            {
                return logBuffer;
            }
            set
            {
                logBuffer = value;
                RaisePropertyChanged();
            }
        }
    }
}
