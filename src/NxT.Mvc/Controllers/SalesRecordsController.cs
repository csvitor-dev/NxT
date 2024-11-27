namespace NxT.Mvc.Controllers
{
    public class SalesRecordsController(SalesRecordService salesRecordService) : Controller
    {
        private readonly SalesRecordService _salesRecordService = salesRecordService;

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> SimpleSearch(DateTime? minimumDate, DateTime? maximumDate)
        {
            if (minimumDate.HasValue == false)
                minimumDate = await _salesRecordService.FindEarliestDateAsync();

            if (maximumDate.HasValue == false)
                maximumDate =  await _salesRecordService.FindLatestDateAsync();

            ViewData["minimumDate"] = $"{minimumDate:yyyy-MM-dd}";
            ViewData["maximumDate"] = $"{maximumDate:yyyy-MM-dd}";

            var result = await _salesRecordService.FindByDateAsync(minimumDate, maximumDate);
            return View(result);
        }

        public async Task<IActionResult> GroupingSearch(DateTime? minimumDate, DateTime? maximumDate)
        {
            if (minimumDate.HasValue == false)
                minimumDate = await _salesRecordService.FindEarliestDateAsync();

            if (maximumDate.HasValue == false)
                maximumDate = await _salesRecordService.FindLatestDateAsync();

            ViewData["minimumDate"] = $"{minimumDate:yyyy-MM-dd}";
            ViewData["maximumDate"] = $"{maximumDate:yyyy-MM-dd}";

            var result = await _salesRecordService.FindByDataGroupingAsync(minimumDate, maximumDate);
            return View(result);
        }
    }
}
