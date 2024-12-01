using System.Diagnostics;
using NxT.Core.Models;
using NxT.Core.ViewModels;
using NxT.Mvc.Services;
using NxT.Mvc.Services.Exceptions;

namespace NxT.Mvc.Controllers;

public class DepartmentsController(DepartmentService departmentService) : Controller
{
    private readonly DepartmentService _departmentService = departmentService;

    // GET: Departments
    public async Task<IActionResult> Index()
    {
        return View(await _departmentService.FindAllAsync());
    }

    // GET: Departments/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Departments/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Department department)
    {
        if (ModelState.IsValid == false)
            return View(department);

        await _departmentService.InsertAsync(department);
        return RedirectToAction(nameof(Index));
    }

    // GET: Departments/Delete/{id?}
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) 
            return RedirectToAction(nameof(Error), new { Message = "ID not provided" });
        var department = await _departmentService.FindByIDAsync(id);

        if (department == null) 
            return RedirectToAction(nameof(Error), new { Message = "ID not found" });

        return View(department);
    }
    // POST: Departments/Delete/{id}
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _departmentService.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }
        catch (IntegrityException ex)
        {
            return RedirectToAction(nameof(Error), new { ex.Message });
        }
        catch (NotFoundException ex)
        {
            return RedirectToAction(nameof(Error), new { ex.Message });
        }
    }

    // GET: Departments/Details/{id?}
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) 
            return RedirectToAction(nameof(Error), new { Message = "ID not provided" });
        var department = await _departmentService.FindByIDAsync(id.Value);

        if (department == null)
            return RedirectToAction(nameof(Error), new { Message = "ID not found" });

        return View(department);
    }

    // GET: Departments/Edit/{id?}
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return RedirectToAction(nameof(Error), new { Message = "ID not provided" });
        var department = await _departmentService.FindByIDAsync(id.Value);

        if (department == null) 
            return RedirectToAction(nameof(Error), new { Message = "ID not found" });
        
        return View(department);
    }

    // POST: Departments/Edit/{id?}
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Department department)
    {
        if (ModelState.IsValid == false)
            return View(department);

        if (id != department.ID)
            return RedirectToAction(nameof(Error), new { Message = "ID mismatch" });
        
        try
        {
            await _departmentService.UpdateAsync(department);
            return RedirectToAction(nameof(Index));
        }
        catch (ApplicationException ex)
        {
            return RedirectToAction(nameof(Error), new { ex.Message });
        }
    }

    public IActionResult Error(string? message)
    {
        Error model = new()
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
            Message = message
        };

        return View(model);
    }
}
