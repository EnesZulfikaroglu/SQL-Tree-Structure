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
            try
            {
                var addedEmployee = _employeeDal.Add(employee);
                return new SuccessDataResult<Employee>(addedEmployee, Messages.AddSuccess);
            }
            catch (Exception)
            {
                return new ErrorDataResult<Employee>(employee, Messages.AddFailure);
            }
        }

        public IResult Delete(Employee employee)
        {
            try
            {
                _employeeDal.Delete(employee);
                return new SuccessResult(Messages.DeleteSuccess);
            }
            catch (Exception)
            {
                return new ErrorResult(Messages.DeleteFailure);
            }
        }
        public IDataResult<Employee> SafeDelete(Employee employee)
        {
            try
            {
                var deletedEmployee = _employeeDal.SafeDelete(employee);
                return new SuccessDataResult<Employee>(deletedEmployee, Messages.DeleteSuccess);
            }
            catch (Exception)
            {
                return new ErrorDataResult<Employee>(employee, Messages.DeleteFailure);
            }
        }

        public IDataResult<Employee> Update(Employee employee)
        {
            try
            {
                var updatedEmployee = _employeeDal.Update(employee);
                return new SuccessDataResult<Employee>(updatedEmployee, Messages.UpdateSuccess);
            }
            catch (Exception)
            {
                return new ErrorDataResult<Employee>(employee, Messages.UpdateFailure);
            }
        }

        public IDataResult<EmployeeDto> GetById(int EmployeeId)
        {
            try
            {
                var result = _mapper.Map<EmployeeDto>(_employeeDal.Get(p => p.Id == EmployeeId));

                return new SuccessDataResult<EmployeeDto>(result);
            }
            catch (Exception)
            {
                var result = _mapper.Map<EmployeeDto>(_employeeDal.Get(p => p.Id == EmployeeId));

                return new ErrorDataResult<EmployeeDto>(result);
            }

        }

        public IDataResult<List<Employee>> GetList()
        {
            try
            {
                return new SuccessDataResult<List<Employee>>(_employeeDal.GetList().ToList());
            }
            catch (Exception)
            {
                return new ErrorDataResult<List<Employee>>(_employeeDal.GetList().ToList());
            }
        }

        public IDataResult<List<ListDto>> GetTree()
        {
            try
            {
                var tree = _employeeDal.GetTree().ToList();

                var result = _mapper.Map<List<Employee>, List<ListDto>>(tree);

                return new SuccessDataResult<List<ListDto>>(result);
            }
            catch (Exception)
            {
                var tree = _employeeDal.GetTree().ToList();

                var result = tree.Select(_mapper.Map<Employee, ListDto>).ToList();

                return new SuccessDataResult<List<ListDto>>(result);
            }
        }
    }
}
