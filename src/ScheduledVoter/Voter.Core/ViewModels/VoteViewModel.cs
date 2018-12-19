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
        public ICommand LoadedCommand { get; private set; }

        public VoteViewModel()
        {
            LoadedCommand = new RelayCommand(async () => await CheckVotesAsync());
        }

        public async Task CheckVotesAsync()
        {
            HttpResponseMessage response = await httpClient.GetAsync(GetUrl(Constants.CheckVotesKey));
            string html = await response.Content.ReadAsStringAsync();

            CanVote = html.Contains(configurationService[Constants.CanVoteKey]);
            Log($"Can vote: {canVote}");
        }

        public async Task VoteAsync()
        {
            foreach (string voteUrl in configurationService.GetArray(Constants.VotesKey))
            {
                Log($"Voting at {voteUrl} ...");
                await httpClient.GetAsync(voteUrl);

                Log($"Waiting {REQUEST_DELAY_MS} ms...");
                await Task.Delay(REQUEST_DELAY_MS);
            }
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
