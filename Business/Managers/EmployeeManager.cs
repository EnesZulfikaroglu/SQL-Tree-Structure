using AutoMapper;
using Business.Constants;
using Business.Services;
using Core.Utilities.Cache;
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
        private ICache _redisCache;
        private bool redisConnection;
        private ILoggerService _logger;

        public EmployeeManager(IEmployeeDal employeeDal, IMapper mapper, ICache redisCache, ILoggerService logger)
        {
            _employeeDal = employeeDal;
            _mapper = mapper;
            _logger = logger;
            _redisCache = redisCache;
            redisConnection = _redisCache.CheckConnectionWithTimeLimit(TimeSpan.FromMilliseconds(100));
        }

        public IDataResult<Employee> Add(EmployeeToAddDto employee)
        {
            var addedEmployee = _employeeDal.Add(_mapper.Map<Employee>(employee));
            
            if (redisConnection)
            {
                _redisCache.FlushAll();
            }

            return new SuccessDataResult<Employee>(addedEmployee, Messages.AddSuccess);
        }

        public IResult Delete(Employee employee)
        {
            _employeeDal.Delete(employee);

            return new SuccessResult(Messages.DeleteSuccess);
        }
        public IDataResult<Employee> SafeDelete(EmployeeToDeleteDto employee)
        {
            var deletedEmployee = _employeeDal.SafeDelete(_mapper.Map<Employee>(employee));

            if (redisConnection)
            {
                _redisCache.FlushAll();
            }

            return new SuccessDataResult<Employee>(deletedEmployee, Messages.DeleteSuccess);

        }

        public IDataResult<Employee> Update(EmployeeToUpdateDto employee)
        {
            var updatedEmployee = _employeeDal.Update(_mapper.Map<Employee>(employee));

            if (redisConnection)
            {
                _redisCache.FlushAll();
            }

            return new SuccessDataResult<Employee>(updatedEmployee, Messages.UpdateSuccess);
        }

        public IDataResult<EmployeeDto> GetById(int id)
        {
            var watch = new System.Diagnostics.Stopwatch();  // To calculate execution time

            if (redisConnection && _redisCache.Exists($"getbyid-{id}"))
            {
                watch.Start();

                Console.WriteLine("Using Redis...");
                _logger.LogInfo("Using Redis...");
             
                var RedisResult = _redisCache.Get<EmployeeDto>($"getbyid-{id}");

                watch.Stop();
                
                Console.WriteLine($"Execution time with Redis: {watch.ElapsedMilliseconds} ms");
                _logger.LogInfo($"Execution time with Redis: {watch.ElapsedMilliseconds} ms");

                return new SuccessDataResult<EmployeeDto>(RedisResult);
            }

            watch.Start();

            Console.WriteLine("Not using Redis...");
            _logger.LogInfo("Not Using Redis...");

            var employee = _employeeDal.Get(e => e.Id == id);

            watch.Stop();
            
            Console.WriteLine($"Execution time without Redis: {watch.ElapsedMilliseconds} ms");
            _logger.LogInfo($"Execution time without Redis: {watch.ElapsedMilliseconds} ms");

            var result = _mapper.Map<EmployeeDto>(employee);

            if (redisConnection)
            {
                _redisCache.Set<EmployeeDto>($"getbyid-{id}", result, DateTime.Now.AddMinutes(60));
            
                Console.WriteLine("data got cached with Redis");
                _logger.LogInfo("data got cached with Redis");
            }
            else
            {
                Console.WriteLine("Can not connect to Redis");
                _logger.LogInfo("Can not connect to Redis");
            }

            return new SuccessDataResult<EmployeeDto>(result);
        }

        public IDataResult<List<Employee>> GetList()
        {
            return new SuccessDataResult<List<Employee>>(_employeeDal.GetList().ToList());
        }

        public IDataResult<List<ListDto>> GetTree()
        {
            var watch = new System.Diagnostics.Stopwatch();  // To calculate execution time

            if (redisConnection && _redisCache.Exists("getall"))
            {
                watch.Start();

                Console.WriteLine("Using Redis...");
                _logger.LogInfo("Using Redis...");
                
                var RedisResult = _redisCache.Get<List<ListDto>>("getall");

                watch.Stop();
                
                Console.WriteLine($"Execution time with Redis: {watch.ElapsedMilliseconds} ms");
                _logger.LogInfo($"Execution time with Redis: {watch.ElapsedMilliseconds} ms");
                
                return new SuccessDataResult<List<ListDto>>(RedisResult);
            }

            watch.Start();

            Console.WriteLine("Not using Redis...");
            _logger.LogInfo("Not Using Redis...");

            var tree = _employeeDal.GetTree().ToList();

            watch.Stop();
            
            Console.WriteLine($"Execution time without Redis: {watch.ElapsedMilliseconds} ms");
            _logger.LogInfo($"Execution time without Redis: {watch.ElapsedMilliseconds} ms");

            var result = _mapper.Map<List<Employee>, List<ListDto>>(tree);

            if (redisConnection)
            {
                _redisCache.Set<List<ListDto>>("getall", result, DateTime.Now.AddMinutes(60));
            
                Console.WriteLine("data got cached with Redis");
                _logger.LogInfo("data got cached with Redis");
            }
            else
            {
                Console.WriteLine("Can not connect to Redis");
                _logger.LogInfo("Can not connect to Redis");
            }

            return new SuccessDataResult<List<ListDto>>(result);
        }
    }
}

