using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Voter.ViewModels.Interfaces;
using Voter.ViewModels.Services;

namespace Voter.ViewModels
{
    public class BaseViewModel : ViewModelBase
    {
        protected readonly ConfigurationService configurationService;
        protected readonly IInteractionService interactionService;
        protected readonly HttpClient httpClient;

        public BaseViewModel()
        {
            configurationService = SimpleIoc.Default.GetInstance<ConfigurationService>();
            interactionService = SimpleIoc.Default.GetInstance<IInteractionService>();
            httpClient = SimpleIoc.Default.GetInstance<HttpClient>();
        }
    }
}
