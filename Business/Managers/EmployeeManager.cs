using Business.Constants;
using Business.Services;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Managers
{
    public class EmployeeManager : IEmployeeService
    {
        private IEmployeeDal _employeeDal;

        public EmployeeManager(IEmployeeDal employeeDal)
        {
            _employeeDal = employeeDal;
        }

        public IResult Add(Employee employee)
        {
            try
            {
                _employeeDal.Add(employee);
                return new SuccessResult(Messages.AddSuccess);
            }
            catch (Exception)
            {
                return new ErrorResult(Messages.AddFailure);
            }
        }

        public IResult SafeAdd(Employee employee)
        {
            try
            {
                _employeeDal.SafeAdd(employee);
                return new SuccessResult(Messages.AddSuccess);
            }
            catch (Exception)
            {
                return new ErrorResult(Messages.AddFailure);
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
        public IResult SafeDelete(Employee employee)
        {
            try
            {
                _employeeDal.SafeDelete(employee);
                return new SuccessResult(Messages.DeleteSuccess);
            }
            catch (Exception)
            {
                return new ErrorResult(Messages.DeleteFailure);
            }
        }

        public IResult Update(Employee employee)
        {
            try
            {
                _employeeDal.Update(employee);
                return new SuccessResult(Messages.UpdateSuccess);
            }
            catch (Exception)
            {
                return new ErrorResult(Messages.UpdateFailure);
            }
        }

        public IDataResult<Employee> GetById(int EmployeeId)
        {
            try
            {
                return new SuccessDataResult<Employee>(_employeeDal.Get(p => p.Id == EmployeeId));
            }
            catch (Exception)
            {
                return new ErrorDataResult<Employee>(_employeeDal.Get(p => p.Id == EmployeeId));
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

        public IDataResult<List<Employee>> GetTree()
        {
            try
            {
                return new SuccessDataResult<List<Employee>>(_employeeDal.GetTree().ToList());
            }
            catch (Exception)
            {
                return new ErrorDataResult<List<Employee>>(_employeeDal.GetTree().ToList());
            }
        }
    }
}
