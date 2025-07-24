using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaraokeTool.Data
{
    internal class Settings
    {
        public string VideoPath { get; set; }

        public List<string> Users { get; set; }

        public Settings() 
        { 
            Users = new List<string>();
            VideoPath = "";
        }
    }
}
