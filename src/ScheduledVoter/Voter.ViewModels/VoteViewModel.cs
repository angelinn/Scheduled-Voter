using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Voter.ViewModels
{
    public class VoteViewModel : BaseViewModel
    {
        private const int REQUEST_DELAY_MS = 5000;

        public async Task CheckVotesAsync()
        {
            
        }

        public async Task VoteAsync()
        {
            foreach (string voteUrl in configurationService.Configuration.Votes)
            {
                await httpClient.GetAsync(voteUrl);
                await Task.Delay(REQUEST_DELAY_MS);
            }
        }
    }
}
