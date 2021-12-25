using Business.Services;
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

        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        /// <summary>
        /// Get the list of all employees - User authentication is required
        /// </summary>
        /// <returns></returns>
        [HttpGet("getall")]
        public IActionResult GetList()
        {        
            var result = _employeeService.GetTree();

            if (result.Success)
            {
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
            var result = _employeeService.GetById(id);

            if (result.Success)
            {
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
            var result = _employeeService.Add(employee);

            if (result.Success)
            {
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
            var result = _employeeService.SafeDelete(employee);

            if (result.Success)
            {
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
            var result = _employeeService.Update(employee);

            if (result.Success)
            {
                return Ok(result.Message + "\nId: " + result.Data.Id);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
    }
}