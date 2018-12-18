using GalaSoft.MvvmLight.Ioc;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Voter.Services;
using Voter.Core.Interfaces;
using Voter.Core.Services;
using System.Net.Http;

namespace Voter
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            ConfigurationService service = new ConfigurationService();
            service.Load();

            SimpleIoc.Default.Register<ConfigurationService>(() => service);
            SimpleIoc.Default.Register<IInteractionService, InteractionService>();
            SimpleIoc.Default.Register<HttpClient>(() => new HttpClient());
        }
    }
}
