using Achieve.IRepository.Base;
using Achieve.Model.PermissionModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Achieve.IRepository
{	
	/// <summary>
	/// IRoleModulePermissionRepository
	/// </summary>	
	public interface IRoleModulePermissionRepository : IBaseRepository<RoleModulePermission>//类名
    {
        Task<List<RoleModulePermission>> WithChildrenModel();
        Task<List<RoleModulePermission>> GetRoleModule();
        Task<List<RoleModulePermission>> RoleModuleMaps();

    }
}
