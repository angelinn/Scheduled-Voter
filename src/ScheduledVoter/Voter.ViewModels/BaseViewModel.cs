using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using System;
using System.Collections.Generic;
using System.Text;
using Voter.ViewModels.Interfaces;
using Voter.ViewModels.Services;

namespace Voter.ViewModels
{
    public class BaseViewModel : ViewModelBase
    {
        protected readonly IInteractionService interactionService;
        public BaseViewModel()
        {
            interactionService = SimpleIoc.Default.GetInstance<IInteractionService>();
        }
    }
}
