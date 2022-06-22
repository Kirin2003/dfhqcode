using Achieve.IRepository.Base;
using Achieve.Model.PermissionModels;
using System.Threading.Tasks;

namespace Achieve.IRepository
{	
	/// <summary>
	/// IRoleRepository
	/// </summary>	
	public interface IRoleRepository : IBaseRepository<Role>//类名
    {

        Task<Role> SaveRole(string roleName);
    }
}
