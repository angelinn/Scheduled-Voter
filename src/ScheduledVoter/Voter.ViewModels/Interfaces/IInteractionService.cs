using System;
using System.Collections.Generic;
using System.Text;

namespace Voter.ViewModels.Interfaces
{
    public interface IInteractionService
    {
        void ShowMessageBox(string title, string message);
    }
}
