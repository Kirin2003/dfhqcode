using AutoMapper;
using Achieve.Model.Seed;
using Achieve.Model.ViewModels;

namespace BackendCode.AutoMapper
{
    public class CustomProfile : Profile
    {
        /// <summary>
        /// 配置构造函数，用来创建关系映射
        /// </summary>
        public CustomProfile()
        {
        }
    }
}
