using Achieve.Model.PermissionModels;
using Achieve.IRepository;
using System.Threading.Tasks;
using System.Linq;
using Achieve.Repository.Base;

namespace Achieve.Repository.Permissions
{
    /// <summary>
    /// UserRoleRepository
    /// </summary>	
    public class UserRoleRepository : BaseRepository<UserRole>, IUserRoleRepository
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="rid"></param>
        /// <returns></returns>
        public async Task<UserRole> SaveUserRole(int uid, int rid)
        {
            UserRole userRole = new UserRole(uid, rid);

            UserRole model = new UserRole();
            var userList = await Query(a => a.UserId == userRole.UserId && a.RoleId == userRole.RoleId);
            if (userList.Count > 0)
            {
                model = userList.FirstOrDefault();
            }
            else
            {
                var id = await Add(userRole);
                model = await QueryById(id);
            }

            return model;

        }
    }
}
