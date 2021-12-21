using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Services
{
    public interface IEmployeeService
    {
        IDataResult<EmployeeDto> GetById(int id);
        IDataResult<List<Employee>> GetList();
        IDataResult<List<ListDto>> GetTree();
        IDataResult<Employee> Add(Employee employee);
        IResult Delete(Employee employee);
        IDataResult<Employee> SafeDelete(Employee employee);
        IDataResult<Employee> Update(Employee employee);
    }
}
