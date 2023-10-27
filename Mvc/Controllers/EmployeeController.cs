using Domain.Dtos;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

public class EmployeeController : Controller
{
   private readonly EmployeeService _employeeService;

   public EmployeeController(EmployeeService employeeService)
   {
      _employeeService = employeeService;
   }


   [HttpGet]
   public async Task<IActionResult> Index()
   {
      var result = await _employeeService.GetListOfEmployees();

      var employees = result.Data.ToList();

      return View(employees);
   }

   [HttpGet]
   public IActionResult Create()
   {
      return View(new EmployeeDto());
   }

   
   [HttpPost]
   public IActionResult Create(EmployeeDto model)
   {
      if (ModelState.IsValid)
      {
         _employeeService.AddEmployee(model);
         return RedirectToAction("Index");
      }
      return View(model);
   }


   
   [HttpGet]
   public async Task<IActionResult> Update(int id)
   {
      var existing = await _employeeService.GetEmployeeById(id);
      
      var employee = existing.Data;

        var result = (new EmployeeDto()
        {
            Id = employee.Id,
            FullName=employee.FullName,
            Number=employee.Number,
            Salary=employee.Salary,
            Email=employee.Email,
            Age=employee.Age,
        });
      return View(result); 
   }

   [HttpPost]
   public async Task<IActionResult> Update(EmployeeDto model)
   {
      if (ModelState.IsValid)
      {
        await _employeeService.UpdateEmployee(model);
         return RedirectToAction("Index");
      }
      return View(model);
   }

   public async Task<ActionResult> Delete(int id)
   {
      await _employeeService.DeleteEmployee(id);
      return RedirectToAction("Index");
   }
}