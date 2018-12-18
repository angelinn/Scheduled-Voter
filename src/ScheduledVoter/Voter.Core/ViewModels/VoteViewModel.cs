using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Voter.Core.ViewModels
{
    public class VoteViewModel : BaseViewModel
    {
        private const int REQUEST_DELAY_MS = 5000;

        public async Task<bool> CheckVotesAsync()
        {
            HttpResponseMessage response = await httpClient.GetAsync(GetUrl(Constants.CheckVotesKey));
            string html = await response.Content.ReadAsStringAsync();

            return html.Contains(Constants.CanVoteKey);
        }

        public async Task VoteAsync()
        {
            foreach (string voteUrl in configurationService.GetArray(Constants.VotesKey))
            {
                await httpClient.GetAsync(voteUrl);
                await Task.Delay(REQUEST_DELAY_MS);
            }
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
