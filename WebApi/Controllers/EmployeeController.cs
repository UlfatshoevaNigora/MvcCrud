using Domain.Dtos;
using Domain.Wraper;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
 [ApiController]
 [Route("[controller]")]
public class EmployeeController
{
     private readonly EmployeeService _employeeService;

    public EmployeeController(EmployeeService employeeService)
    {
        _employeeService = employeeService;
    }


    [HttpPost("AddEmployee")]
     public async Task<Response<EmployeeDto>> AddEmployee (EmployeeDto model)
    {
        return await _employeeService.AddEmployee(model);
    }


    [HttpPut("UpdateEmployee")]
    public async Task<Response<EmployeeDto>> UpdateEmployee (EmployeeDto employee)
    {
        return await _employeeService.UpdateEmployee(employee);
    }

    [HttpGet("GetEmployees")]
    public async Task<Response<List<EmployeeDto>>> GetListOfEmployees()
    {
        return await _employeeService.GetListOfEmployees();
    }


    [HttpGet("GetById")] 
    public async Task<Response<EmployeeDto>> GetEmployeeById (int id)
    {
        return await _employeeService.GetEmployeeById(id);
    }


    [HttpDelete("DeleteEmployee")]
    public async Task<Response<bool>> DeleteEmployee (int id)
    {
        return await _employeeService.DeleteEmployee(id);
    }
}