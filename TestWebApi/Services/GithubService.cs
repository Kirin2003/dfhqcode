using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendCode.Base;
using BackendCode.Models;
using Nest;

namespace BackendCode.Services
{
    public class GithubService : EsBase<GithubRepository>
    {
        

        public override string IndexName => "github";
    }
}
