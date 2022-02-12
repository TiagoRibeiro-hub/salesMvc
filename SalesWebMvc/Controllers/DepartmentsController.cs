namespace SalesWebMvc.Controllers;
public class DepartmentsController : Controller
{
    private readonly ErrorViewModel _errorViewModel;
    private readonly IDepartmentsService _departmentsService;

    public DepartmentsController(IDepartmentsService departmentsService, ErrorViewModel errorViewModel)
    {
        _departmentsService = departmentsService;
        _errorViewModel = errorViewModel;
    }

    [HttpGet]
    [Route("/Departments")]
    public async Task<IActionResult> Departments()
    {
        try
        {
            var departments = await _departmentsService.DepartmentsToListAsync();
            if (departments.Any())
            {
                return View(departments);
            }
            return RedirectToAction("Error", "Home");
        }
        catch (Exception)
        {
            return RedirectToAction("Error", "Home");
        }
    }

    [HttpGet]
    public async Task<IActionResult> Details(int? id)
    {
        return await FindDepartmentAsync(id);
    }

    #region Create
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name")] DepartmentViewModel model)
    {
        try
        {
            bool isRegistered = false;
            if (ModelState.IsValid)
            {
                if (model is not null)
                {
                    isRegistered = await _departmentsService.IsDepartmentRegisteredAsync(model);
                    if (isRegistered == false)
                    {
                        bool res = await _departmentsService.InsertDepartmentAsync(model);
                        if (res)
                        {
                            return RedirectToAction(nameof(Departments));
                        }
                    }
                }
            }
            if (isRegistered)
            {
                TempData["Error"] = _errorViewModel.DepartmentIsAlreadyRegistered();
            }
            else
            {
                TempData["Error"] = _errorViewModel.SomethingWentWrong();
            }
            return View(model);
        }
        catch (Exception)
        {
            return RedirectToAction("Error", "Home");
        }

    }
    #endregion

    #region Edit
    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        return await FindDepartmentAsync(id);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit([Bind("Id,Name")] DepartmentViewModel model)
    {
        try
        {
            if (ModelState.IsValid)
            {
                if (model is not null)
                {
                    if (model.Id > 0)
                    {
                        var res = await _departmentsService.EditDepartmentAsync(model);
                        if (res)
                        {
                            return RedirectToAction(nameof(Departments));
                        }
                    }
                }
            }
            TempData["Error"] = _errorViewModel.SomethingWentWrong();
            return View(model);
        }
        catch (Exception)
        {
            return RedirectToAction("Error", "Home");
        }
    }
    #endregion

    #region Delete
    [HttpGet]
    public async Task<IActionResult> DeleteView(int? id)
    {
        return await FindDepartmentAsync(id);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int? id)
    {
        try
        {
            if (ModelState.IsValid)
            {
                if (id is not null)
                {
                    var res = await _departmentsService.DeleteDepartmentAsync(id);
                    if (res)
                    {
                        return RedirectToAction(nameof(Departments));
                    }
                }
            }
            TempData["Error"] = _errorViewModel.SomethingWentWrongWithDelete();
            return RedirectToAction(nameof(Departments));
        }
        catch (Exception)
        {
            return RedirectToAction("Error", "Home");
        }
    }
    #endregion

    private async Task<IActionResult> FindDepartmentAsync(int? id)
    {
        try
        {
            if (id is not null)
            {
                var department = await _departmentsService.FindDepartmentAsync(id);
                if (department is not null)
                {
                    return View(department);
                }
            }
            TempData["Error"] = _errorViewModel.SomethingWentWrong();
            return RedirectToAction(nameof(Departments));
        }
        catch (Exception)
        {
            return RedirectToAction("Error", "Home");
        }
    }
}

