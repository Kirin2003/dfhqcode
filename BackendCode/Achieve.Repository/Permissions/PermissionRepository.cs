using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Achieve.Repository.Base;
using Achieve.IRepository;
using Achieve.Model.PermissionModels;

namespace Achieve.Repository.Permissions
{
    public class PermissionRepository : BaseRepository<Permission>, IPermissionRepository
    {
    }
}
