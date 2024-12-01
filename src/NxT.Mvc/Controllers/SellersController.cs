using System.Diagnostics;
using NxT.Core.Models;
using NxT.Core.ViewModels;
using NxT.Mvc.Services;
using NxT.Mvc.Services.Exceptions;

namespace NxT.Mvc.Controllers;

public class SellersController(SellerService sellerService, DepartmentService departmentService) : Controller
{
    private readonly SellerService _sellerService = sellerService;
    private readonly DepartmentService _departmentService = departmentService;

    // GET: Sellers
    public async Task<IActionResult> Index()
    {
        return View(await _sellerService.FindAllAsync());
    }

    // GET: Sellers/Create
    public async Task<IActionResult> Create()
    {
        var departments = await _departmentService.FindAllAsync();
        var sellerViewModel = new SellerForm(departments);

        return View(sellerViewModel);
    }
    // POST: Sellers/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost] // Annotation
    [ValidateAntiForgeryToken] // CSRF
    public async Task<IActionResult> Create(Seller seller)
    {
        if (ModelState.IsValid == false)
        {
            var departments = await _departmentService.FindAllAsync();

            return View(new SellerForm(departments)
            {
                Seller = seller
            });
        }

        await _sellerService.InsertAsync(seller);
        return RedirectToAction(nameof(Index));
    }

    // GET: Sellers/Delete/{id?}
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) 
            return RedirectToAction(nameof(Error), new { Message = "ID not provided" });
        var seller = await _sellerService.FindByIDAsync(id.Value);

        if (seller == null) 
            return RedirectToAction(nameof(Error), new { Message = "ID not found" });

        return View(seller);
    }
    // POST: Sellers/Delete/{id}
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _sellerService.RemoveAsync(id);
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

    // GET: Sellers/Details/{id?}
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) 
            return RedirectToAction(nameof(Error), new { Message = "ID not provided" });
        var seller = await _sellerService.FindByIDAsync(id.Value);

        if (seller == null) return RedirectToAction(nameof(Error), new { Message = "ID not found" });

        return View(seller);
    }

    // GET: Sellers/Edit/{id?}
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) 
            return RedirectToAction(nameof(Error), new { Message = "ID not provided" });
        var seller = await _sellerService.FindByIDAsync(id.Value);

        if (seller == null) 
            return RedirectToAction(nameof(Error), new { Message = "ID not found" });
        var departments = await _departmentService.FindAllAsync();
        SellerForm model = new(departments) { Seller = seller };

        return View(model);
    }
    // POST: Sellers/Edit/{id?}
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Seller seller)
    {
        if (ModelState.IsValid == false)
        {
            var departments = await _departmentService.FindAllAsync();

            return View(new SellerForm(departments)
            {
                Seller = seller
            });
        }

        if (id != seller.ID)
            return RedirectToAction(nameof(Error), new { Message = "ID mismatch" });
        try
        {
            await _sellerService.UpdateAsync(seller);
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
