using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repos.Interface
{
    public interface IReadOperation<T> where T : class
    {
        public Task<IEnumerable<T>> GetAllRecordsTask();

        public Task<T> GetByConditionTask(Expression<Func<T, bool>> expression);

        public Task<IEnumerable<T>> GetAllRecordsByConditionTask(Expression<Func<T, bool>> expression); 

        
    }
}
