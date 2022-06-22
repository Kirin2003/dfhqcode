using System;
using System.Threading.Tasks;
using Achieve.IRepository.Base;
using Achieve.Model.PermissionModels;

namespace Achieve.IRepository
{	
	/// <summary>
	/// IUserRoleRepository
	/// </summary>	
	public interface IUserRoleRepository : IBaseRepository<UserRole>//类名
    {
        Task<UserRole> SaveUserRole(int uid, int rid);


    }
}

	