using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendCode.Models
{
    public class GithubRepository
    {
        public string url { get; set; }
        public string name { get; set; }
        public string doi { get; set; }

        public override string ToString()
        {
            return name+" "+url;
        }
    }

     
}
