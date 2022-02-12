namespace SalesWebMvc.Controllers;
public class SalesRecordsController : Controller
{
    private readonly ISalesRecordsService _salesRecordsService;

    public SalesRecordsController(ISalesRecordsService salesRecordsService)
    {
        _salesRecordsService = salesRecordsService;
    }

    [Route("/SalesRecords")]
    [HttpGet]
    public async Task<IActionResult> SalesRecords()
    {
        try
        {
            var salesRecords = await _salesRecordsService.GetAllRecordsAsync();
            if(salesRecords is null)
            {
                return RedirectToAction("Error", "Home");
            }
            ViewData["amountSum"] = salesRecords.Sum(x => x.Amount).ToString("F2");
            return View(salesRecords);
        }
        catch (Exception)
        {
            throw new Exception();
        }
        
    }

    [HttpGet]
    public async Task<IActionResult> SimpleSearch(DateTime? minDate, DateTime? maxDate)
    {
        try
        {
            if (!minDate.HasValue)
            {
                minDate = new DateTime(DateTime.Now.Year, 1, 1);
            }
            if (!maxDate.HasValue)
            {
                maxDate = DateTime.Now;
            }
            var res = _salesRecordsService.FindByDateAsync(minDate, maxDate);
            ViewData["minDate"] = minDate.Value.ToString("yyyy-MM-dd");
            ViewData["maxDate"] = maxDate.Value.ToString("yyyy-MM-dd");
            var search = await res;
            ViewData["amountSum"] = search.Sum(x => x.Amount).ToString("F2");
            return View(search);
        }
        catch (Exception)
        {
            throw new Exception();
        }
    }
}
