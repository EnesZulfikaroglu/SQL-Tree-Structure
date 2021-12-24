using AutoMapper;
using Business.Constants;
using Business.Services;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Managers
{
    public class EmployeeManager : IEmployeeService
    {
        private IEmployeeDal _employeeDal;
        private IMapper _mapper;

        public EmployeeManager(IEmployeeDal employeeDal, IMapper mapper)
        {
            _employeeDal = employeeDal;
            _mapper = mapper;
        }

        public IDataResult<Employee> Add(Employee employee)
        {
            var addedEmployee = _employeeDal.Add(employee);
            return new SuccessDataResult<Employee>(addedEmployee, Messages.AddSuccess);
        }

        public IResult Delete(Employee employee)
        {
            _employeeDal.Delete(employee);
            return new SuccessResult(Messages.DeleteSuccess);
        }
        public IDataResult<Employee> SafeDelete(Employee employee)
        {
            var deletedEmployee = _employeeDal.SafeDelete(employee);
            return new SuccessDataResult<Employee>(deletedEmployee, Messages.DeleteSuccess);

        }

        public IDataResult<Employee> Update(Employee employee)
        {
            var updatedEmployee = _employeeDal.Update(employee);
            return new SuccessDataResult<Employee>(updatedEmployee, Messages.UpdateSuccess);
        }

        public IDataResult<EmployeeDto> GetById(int EmployeeId)
        {
            var result = _mapper.Map<EmployeeDto>(_employeeDal.Get(p => p.Id == EmployeeId));

            return new SuccessDataResult<EmployeeDto>(result);
        }

        public IDataResult<List<Employee>> GetList()
        {
            return new SuccessDataResult<List<Employee>>(_employeeDal.GetList().ToList());
        }

        public IDataResult<List<ListDto>> GetTree()
        {
            var tree = _employeeDal.GetTree().ToList();

            var result = _mapper.Map<List<Employee>, List<ListDto>>(tree);
            return new SuccessDataResult<List<ListDto>>(result);
        }
    }
}
