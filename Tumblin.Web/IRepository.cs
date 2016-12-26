using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tumblin.Web
{
    public interface IRepository<T>
    {
        Task<T> Get(int id);
        Task<IEnumerable<T>> Find();
        Task<T> Add(T model);
        Task Remove(int id);
        Task<T> Update(T model);
    }
}
