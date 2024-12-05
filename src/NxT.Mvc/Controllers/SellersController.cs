using System.Diagnostics;
using NxT.Core.Models;
using NxT.Core.ViewModels;
using NxT.Exception.Internal;
using NxT.Infrastructure.Data.Repositories;
using NxT.Infrastructure.Data.Services;

namespace NxT.Mvc.Controllers;

public class SellersController
    (SellerRepository _service, CommitPersistence _commit) : Controller
{
    // GET: Sellers
    public async Task<IActionResult> Index() 
        => View(await _service.FindAllAsync());

    // GET: Sellers/Create
    public async Task<IActionResult> Create()
    {
        var departments = await _service.FindDepartments();
        var model = new SellerForm(departments);

        return View(model);
    }
    // POST: Sellers/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost] // Annotation
    [ValidateAntiForgeryToken] // CSRF
    public async Task<IActionResult> Create(Seller seller)
    {
        if (ModelState.IsValid is false)
        {
            var departments = await _service.FindDepartments();

            return View(new SellerForm(departments)
            {
                Seller = seller
            });
        }

        await _service.InsertAsync(seller);
        await _commit.CommitAsync();

        return RedirectToAction(nameof(Index));
    }

    // GET: Sellers/Delete/{id?}
    public async Task<IActionResult> Delete(int? id)
    {
        if (id is null) 
            return RedirectToAction(nameof(Error), new { Message = "ID not provided" });
        var seller = await _service.FindByIdAsync(id);

        if (seller is null) 
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
            await _service.RemoveAsync(id);
            await _commit.CommitAsync();
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
        if (id is null) 
            return RedirectToAction(nameof(Error), new { Message = "ID not provided" });
        var seller = await _service.FindByIdAsync(id.Value);

        if (seller is null) return RedirectToAction(nameof(Error), new { Message = "ID not found" });

        return View(seller);
    }

    // GET: Sellers/Edit/{id?}
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) 
            return RedirectToAction(nameof(Error), new { Message = "ID not provided" });
        var seller = await _service.FindByIdAsync(id.Value);

        if (seller == null) 
            return RedirectToAction(nameof(Error), new { Message = "ID not found" });
        var departments = await _service.FindDepartments();
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
            var departments = await _service.FindDepartments();

            return View(new SellerForm(departments)
            {
                Seller = seller
            });
        }

        if (id != seller.ID)
            return RedirectToAction(nameof(Error), new { Message = "ID mismatch" });
        try
        {
            await _service.UpdateAsync(seller);
            await _commit.CommitAsync();
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
