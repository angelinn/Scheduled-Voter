using System;
using System.Collections.Generic;
using System.Text;

namespace Voter.Core.Interfaces
{
    public interface IInteractionService
    {
        void ShowMessageBox(string title, string message);
        void ChangeWindow(string title);
    }
}
