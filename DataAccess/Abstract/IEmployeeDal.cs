using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Core.DataAccess;
using Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface IEmployeeDal : IRepository<Employee>
    {
        public void SafeAdd(Employee employee);
        public void SafeDelete(Employee employee);
        public IList<Employee> GetTree();
        public void TreeHelper(Employee parent);
    }
}
