using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Voter.Core.Interfaces;

namespace Voter.Services
{
    public class InteractionService : IInteractionService
    {
        public void ChangeWindow(string title)
        {
            Window window = Activator.CreateInstance(Type.GetType($"Voter.{title}Window")) as Window;
            window.Show();
        }

        public void ShowMessageBox(string title, string message)
        {
            MessageBox.Show(message, title);
        }
    }
}
