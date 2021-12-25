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
        IDataResult<Employee> Add(EmployeeToAddDto employee);
        IResult Delete(Employee employee);
        IDataResult<Employee> SafeDelete(EmployeeToDeleteDto employee);
        IDataResult<Employee> Update(EmployeeToUpdateDto employee);
    }
}
