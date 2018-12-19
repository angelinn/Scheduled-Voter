using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Voter.Core.Messages;

namespace Voter
{
    /// <summary>
    /// Interaction logic for VoteWindow.xaml
    /// </summary>
    public partial class VoteWindow : Window
    {
        public VoteWindow()
        {
            InitializeComponent();
            Messenger.Default.Register<LogAddedMessage>(this, m => OnLogAdded());
        }

        private void OnLogAdded()
        {
            svLog.ScrollToBottom();
        }

        private void ColumnDefinition_MouseWheel(object sender, MouseWheelEventArgs e)
        {

        }
    }
}
