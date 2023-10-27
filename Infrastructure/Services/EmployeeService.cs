using System.Net;
using Domain.Dtos;
using Domain.Entities;
using Domain.Wraper;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class EmployeeService
{
    private readonly DataContext _dataContext;
    public EmployeeService(DataContext dataContext)
    {
        _dataContext = dataContext;
    } 

        public async Task<Response<EmployeeDto>> AddEmployee(EmployeeDto employee)
    {
        try
        {
            var employees = new Employee()
            {
                Id = employee.Id,
                FullName = employee.FullName,
                Age = employee.Age,
                Number = employee.Number,
                Salary = employee.Salary,
                Email=employee.Email,

            };
            await _dataContext.Employees.AddAsync(employees);
            await _dataContext.SaveChangesAsync();
            return new Response<EmployeeDto>(employee);
        }
        catch (Exception ex)
        {
            return new Response<EmployeeDto>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    public async Task<Response<bool>> DeleteEmployee(int id)
    {
        try
        {
            var employee = await _dataContext.Employees.FindAsync(id);
            if (employee != null)
            {
                _dataContext.Employees.Remove(employee);
                var result = await _dataContext.SaveChangesAsync();
                var response = result == 1;
                return new Response<bool>(response);
            }
            else
            {
                return new Response<bool>(HttpStatusCode.BadRequest, "öyle bir çalışan yok");
            }
        }
        catch (Exception ex)
        {
            return new Response<bool>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }

        public async Task<Response<EmployeeDto>> GetEmployeeById(int id)
    {
        try
        {
            var response = await _dataContext.Employees.Select(x => new EmployeeDto()
            {
                Id = x.Id,
                FullName = x.FullName,
                Age = x.Age,
                Salary = x.Salary,
                Number = x.Number,
                Email= x.Email,

            }).FirstOrDefaultAsync(p => p.Id == id);
            return new Response<EmployeeDto>(response);
        }
        catch (Exception ex)
        {
            return new Response<EmployeeDto>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }


       public async Task<Response<List<EmployeeDto>>> GetListOfEmployees()
    {
        try
        {
            var model = await _dataContext.Employees.Select(o => new EmployeeDto()
            {
                Id = o.Id,
                FullName = o.FullName,
                Salary = o.Salary,
                Age = o.Age,
                Number = o.Number,
                Email= o.Email,
            }).ToListAsync();

            return new Response<List<EmployeeDto>>(model);

        }
        catch (Exception ex)
        {
            return new Response<List<EmployeeDto>>(HttpStatusCode.InternalServerError, ex.Message);
        }

    }

      public async Task<Response<EmployeeDto>> UpdateEmployee(EmployeeDto employee)
    {
        try
        {
            var find = await _dataContext.Employees.FindAsync(employee.Id);
            if (find != null)
            {
                find.FullName = employee.FullName;
                find.Age=employee.Age;
                find.Number=employee.Number;
                find.Salary=employee.Salary;
                find.Email=employee.Email;
                await _dataContext.SaveChangesAsync();
                var response = employee;
                return new Response<EmployeeDto>(response);
            }
            else
            {
                return new Response<EmployeeDto>(HttpStatusCode.BadRequest, "öyle bir çalışan yok");
            }

        }
        catch (Exception ex)
        {
            return new Response<EmployeeDto>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }
}
