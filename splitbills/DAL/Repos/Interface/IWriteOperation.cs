using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repos.Interface
{
    public interface IWriteOperation<T> where T : class
    {
        public Task<bool> AddTask(T entity);
        public Task<bool> RemoveTask(T entity);
        
    }
}
