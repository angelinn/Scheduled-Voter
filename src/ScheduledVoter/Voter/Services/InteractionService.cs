﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Voter.ViewModels.Interfaces;

namespace Voter.Services
{
    public class InteractionService : IInteractionService
    {
        public void ShowMessageBox(string title, string message)
        {
            MessageBox.Show(message, title);
        }
    }
}