using NxT.Infrastructure.Data.Repositories;

namespace NxT.Mvc.Controllers;

public class SalesRecordsController(SalesRecordRepository _service) : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public async Task<IActionResult> SimpleSearch(DateTime? minimumDate, DateTime? maximumDate)
    {
        if (minimumDate.HasValue is false)
            minimumDate = await _service.FindEarliestDateAsync();

        if (maximumDate.HasValue is false)
            maximumDate =  await _service.FindLatestDateAsync();

        ViewData["minimumDate"] = $"{minimumDate:yyyy-MM-dd}";
        ViewData["maximumDate"] = $"{maximumDate:yyyy-MM-dd}";

        var result = await _service.FindByDateAsync(minimumDate, maximumDate);
        return View(result);
    }

    public async Task<IActionResult> GroupingSearch(DateTime? minimumDate, DateTime? maximumDate)
    {
        if (minimumDate.HasValue is false)
            minimumDate = await _service.FindEarliestDateAsync();

        if (maximumDate.HasValue is false)
            maximumDate = await _service.FindLatestDateAsync();

        ViewData["minimumDate"] = $"{minimumDate:yyyy-MM-dd}";
        ViewData["maximumDate"] = $"{maximumDate:yyyy-MM-dd}";

        var result = await _service.FindByGroupingDateAsync(minimumDate, maximumDate);
        return View(result);
    }
}
