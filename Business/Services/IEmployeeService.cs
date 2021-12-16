using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Services
{
    public interface IEmployeeService
    {
        IDataResult<Employee> GetById(int id);
        IDataResult<List<Employee>> GetList();
        IDataResult<List<Employee>> GetTree();
        IResult Add(Employee employee);
        IResult SafeAdd(Employee employee);
        IResult Delete(Employee employee);
        IResult SafeDelete(Employee employee);
        IResult Update(Employee employee);
    }
}
