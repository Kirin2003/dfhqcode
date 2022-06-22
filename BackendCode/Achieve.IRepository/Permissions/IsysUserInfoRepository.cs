using Achieve.IRepository.Base;
using Achieve.Model.PermissionModels;
using System.Threading.Tasks;

namespace Achieve.IRepository
{	
	/// <summary>
	/// ISysAdminRepository
	/// </summary>	
	public interface ISysAdminRepository : IBaseRepository<SysAdmin>//类名
    {

        Task<SysAdmin> SaveUserInfo(string loginName, string loginPwd);
        Task<string> GetUserRoleNameStr(string loginName, string loginPwd);

    }
}
