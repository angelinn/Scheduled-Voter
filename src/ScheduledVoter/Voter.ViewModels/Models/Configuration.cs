using System;
using System.Collections.Generic;
using System.Text;

namespace Voter.ViewModels.Models
{
    public class Configuration
    {
        public string Base { get; set; }
        public string Login { get; set; }
        public string LoginPost { get; set; }
        public List<string> Votes { get; set; }

        public string LoginFull => Base + Login;
        public string LoginPostFull => Base + LoginPost;
    }
}
