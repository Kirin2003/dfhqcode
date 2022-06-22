using Achieve.IRepository.Base;
using Achieve.Model;
using Achieve.Model.EntityModels;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Achieve.IRepository
{
    public partial interface IPaperClassRepository : IBaseRepository<PaperClass>
    {
        Task<PageModel<PaperClass>> GetQueryPageOfMapperTb(Expression<Func<PaperClass, bool>> whereExpression, int intPageIndex = 1, int intPageSize = 20, string strOrderByFileds = null);
    }
}