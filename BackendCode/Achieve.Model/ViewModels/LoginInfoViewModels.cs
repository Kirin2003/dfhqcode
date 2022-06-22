using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Achieve.Model.ViewModels
{
    //登录视图模型
    public class LoginInfoViewModels
    {
        public string uLoginName { get; set; }

        public string uLoginPwd { get; set; }

        public string VCode { get; set; }

        public bool IsMember { get; set; }
    }
}
