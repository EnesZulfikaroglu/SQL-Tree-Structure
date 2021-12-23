using AutoMapper;
using Business.Services;
using Core.Utilities.Cache;
using Entities.Concrete;
using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private IEmployeeService _employeeService;
        private IMapper _mapper;
        //private IRedisService _redisService;
        private ICache _redisCache;
        private bool redisConnection;

        public EmployeesController(IEmployeeService employeeService, ICache redisCache, IMapper mapper)
        {
            _employeeService = employeeService;
            _mapper = mapper;
            _redisCache = redisCache;
            redisConnection = _redisCache.CheckConnectionWithTimeLimit(TimeSpan.FromMilliseconds(100));
        }

        /// <summary>
        /// Get the list of all employees - User authentication is required
        /// </summary>
        /// <returns></returns>
        [HttpGet("getall")]
        public IActionResult GetList()
        {
            var watch = new System.Diagnostics.Stopwatch();  // To calculate execution time

            if (redisConnection && _redisCache.Exists("getall"))
            {
                watch.Start();

                Console.WriteLine("Using Redis...");
                var RedisResult = _redisCache.Get<List<ListDto>>("getall");

                watch.Stop();
                Console.WriteLine($"Execution time with Redis: {watch.ElapsedMilliseconds} ms");

                return Ok(RedisResult);
            }

            watch.Start();

            Console.WriteLine("Not using Redis...");
            var result = _employeeService.GetTree();

            watch.Stop();
            Console.WriteLine($"Execution time without Redis: {watch.ElapsedMilliseconds} ms");

            if (result.Success)
            {
                if (redisConnection)
                {
                    _redisCache.Set<List<ListDto>>("getall", result.Data, DateTime.Now.AddMinutes(60));
                    Console.WriteLine("data got cached with Redis");
                }
                else
                {
                    Console.WriteLine("Can not connect to Redis");
                }
                return Ok(result.Data);
            }
            else
            {

                return BadRequest(result.Message);
            }

        }

        /// <summary>
        /// Get the employee information by its id - User authentication is required
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("getbyid")]
        public IActionResult Get(int id)
        {
            var watch = new System.Diagnostics.Stopwatch();  // To calculate execution time

            if (redisConnection && _redisCache.Exists($"getbyid-{id}"))
            {
                watch.Start();

                Console.WriteLine("Using Redis...");
                var RedisResult = _redisCache.Get<EmployeeDto>($"getbyid-{id}");

                watch.Stop();
                Console.WriteLine($"Execution time with Redis: {watch.ElapsedMilliseconds} ms");

                return Ok(RedisResult);
            }

            watch.Start();

            Console.WriteLine("Not using Redis...");
            var result = _employeeService.GetById(id);

            watch.Stop();
            Console.WriteLine($"Execution time without Redis: {watch.ElapsedMilliseconds} ms");

            if (result.Success)
            {
                if (redisConnection)
                {
                    _redisCache.Set<EmployeeDto>($"getbyid-{id}", result.Data, DateTime.Now.AddMinutes(60));
                    Console.WriteLine("data got cached with Redis");
                }
                else
                {
                    Console.WriteLine("Can not connect to Redis");
                }
                return Ok(result.Data);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }


        /// <summary>
        /// Add a new employee to database
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        [HttpPost("add")]
        public IActionResult Add(EmployeeToAddDto employee)
        {
            var result = _employeeService.Add(_mapper.Map<Employee>(employee));

            if (result.Success)
            {
                if (redisConnection)
                {
                    _redisCache.FlushAll();
                }
                return Ok(result.Message + "\nId: " + result.Data.Id);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        /// <summary>
        /// Delete an employee from database
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        [HttpPost("delete")]
        public IActionResult Delete(EmployeeToDeleteDto employee)
        {
            var result = _employeeService.SafeDelete(_mapper.Map<Employee>(employee));

            if (result.Success)
            {
                if (redisConnection)
                {
                    _redisCache.FlushAll();
                }
                return Ok(result.Message + "\nId: " + result.Data.Id);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        /// <summary>
        /// Update an employee on database
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        [HttpPost("update")]
        public IActionResult Update(EmployeeToUpdateDto employee)
        {
            var result = _employeeService.Update(_mapper.Map<Employee>(employee));

            if (result.Success)
            {
                if (redisConnection)
                {
                    _redisCache.FlushAll();
                }
                return Ok(result.Message + "\nId: " + result.Data.Id);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
    }
}