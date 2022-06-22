using Achieve.IRepository;
using Achieve.Model.PermissionModels;
using System.Threading.Tasks;
using System.Linq;
using Achieve.Repository.Base;

namespace Achieve.Repository.Permissions
{
    /// <summary>
    /// RoleRepository
    /// </summary>	
    public class RoleRepository : BaseRepository<Role>, IRoleRepository
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public async Task<Role> SaveRole(string roleName)
        {
            Role role = new Role(roleName);
            Role model = new Role();
            var userList = await Query(a => a.Name == role.Name && a.Enabled);
            if (userList.Count > 0)
            {
                model = userList.FirstOrDefault();
            }
            else
            {
                var id = await Add(role);
                model = await QueryById(id);
            }

            return model;

        }
    }
}
