namespace SalesWebMvc.Controllers;
public class SellersController : Controller
{
    private readonly ISellersService _sellerServices;
    private readonly IDepartmentsService _departmentsService;
    private readonly ErrorViewModel _errorViewModel;
    public SellersController(
        ISellersService sellerServices, IDepartmentsService departmentsService,
        ErrorViewModel errorViewModel)
    {
        _sellerServices = sellerServices;
        _departmentsService = departmentsService;
        _errorViewModel = errorViewModel;
    }

    [HttpGet]
    [Route("/Sellers")]
    public async Task<IActionResult> Sellers(int skip=0, int take=8)
    {
        try
        {
            var sellers = await _sellerServices.SellersToListAsync();
            if (sellers.Any())
            {
                return View(sellers);
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
        try
        {
            if (id is not null)
            {
                var seller = await GetSellerAsync(id);
                if (seller is not null)
                {
                    return View(seller);
                }
            }
            TempData["Error"] = _errorViewModel.SomethingWentWrong();
            return RedirectToAction(nameof(Sellers));
        }
        catch (Exception)
        {
            return RedirectToAction("Error", "Home");
        }
    }

    #region Create
    [HttpGet]
    public async Task<IActionResult> CreateView(SellerViewModel? model)
    {
        try
        {
            var departments = await GetDepartmentsAsync();
            if (departments is not null)
            {
                var viewModel = new SellerViewModel()
                {
                    Departments = departments
                };
                ViewData["BirthDate"] = "yyyy-MM-dd";
                if (!string.IsNullOrWhiteSpace(model.Email))
                {
                    viewModel.Name = model!.Name;
                    viewModel.Email = model.Email;
                    viewModel.BirthDate = model.BirthDate;
                    viewModel.BaseSalary = model.BaseSalary;
                    ViewData["BirthDate"] = model.BirthDate.ToString("yyyy-MM-dd");
                }    
                return View(viewModel);
            }
            TempData["Error"] = _errorViewModel.SomethingWentWrong();
            return RedirectToAction(nameof(Sellers));
        }
        catch (Exception)
        {
            return RedirectToAction("Error", "Home");
        }

    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Name,Email,BirthDate,BaseSalary,DepartmentId")] SellerViewModel model)
    {
        try
        {
            if(model.DepartmentId == 0)
            {
                TempData["Error"] = _errorViewModel.ChooseDepartmentOption();
                return RedirectToAction("CreateView", model);
            }
            bool isRegistered = false;
            if (ModelState.IsValid)
            {
                if (model is not null)
                {
                    isRegistered = await _sellerServices.IsSellerRegisteredAsync(model);
                    if (isRegistered == false)
                    {
                        bool res = await _sellerServices.InsertSellerAsync(model);
                        if (res)
                        {
                            return RedirectToAction(nameof(Sellers));
                        }
                    }
                }
            }
            if (isRegistered)
            {
                TempData["Error"] = _errorViewModel.SellerIsAlreadyRegistered();
            }
            else
            {
                TempData["Error"] = _errorViewModel.SomethingWentWrong();
            }
            return RedirectToAction(nameof(Create));
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
        try
        {
            if (id is null)
            {
                return RedirectToAction("Error", "Home");
            }
            var seller = await GetSellerAsync(id);
            if (seller is not null)
            {
                var departments = await GetDepartmentsAsync();
                if (departments is not null)
                {
                    seller.Departments = departments;
                    return View(seller);
                }
            }
            TempData["Error"] = _errorViewModel.SomethingWentWrong();
            return RedirectToAction(nameof(Sellers));
        }
        catch (Exception ex)
        {
            return RedirectToAction("Error", "Home");
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit([Bind("Id,Name,Email,BirthDate,BaseSalary,DepartmentId")] SellerViewModel model)
    {
        try
        {
            if (ModelState.IsValid)
            {
                if (model is not null)
                {
                    if (model.Id > 0)
                    {
                        var res = await _sellerServices.EditSellerAsync(model);
                        if (res)
                        {
                            return RedirectToAction(nameof(Sellers));
                        }
                    }
                }
            }
            TempData["Error"] = _errorViewModel.SomethingWentWrong();
            return View(model);
        }
        catch (Exception ex)
        {
            return RedirectToAction("Error", "Home");
        }
    }
    #endregion

    #region Delete
    [HttpGet]
    public async Task<IActionResult> DeleteView(int? id)
    {
        try
        {
            if (id is not null)
            {
                var seller = await GetSellerAsync(id);
                if (seller is not null)
                {
                    return View(seller);
                }
            }
            TempData["Error"] = _errorViewModel.SomethingWentWrong();
            return RedirectToAction(nameof(Sellers));
        }
        catch (Exception ex)
        {
            return RedirectToAction("Error", "Home");
        }
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
                    var res = await _sellerServices.DeleteSellerAsync(id);
                    if (res)
                    {
                        return RedirectToAction(nameof(Sellers));
                    }
                }
            }
            TempData["Error"] = _errorViewModel.SomethingWentWrongWithDelete();
            return RedirectToAction(nameof(Sellers));
        }
        catch (IntegrityException ex)
        {
            TempData["Error"] = ex.Message;
            return RedirectToAction(nameof(Sellers));
        }

    }
    #endregion

    private async Task<SellerViewModel> GetSellerAsync(int? id)
    {
        var model = await _sellerServices.GetSellerAsync(id);
        return model;
    }
    private async Task<SellerViewModel> FindSellerAsync(int? id)
    {
        try
        {
            if (id is not null)
            {
                var department = await _sellerServices.FindSellerAsync(id);
                if (department is not null)
                {
                    return department;
                }
            }
            return null!;
        }
        catch (Exception)
        {
            throw new Exception();
        }
    }
    private async Task<List<DepartmentViewModel>> GetDepartmentsAsync()
    {
        var departments = await _departmentsService.DepartmentsToListAsync();
        departments.OrderBy(x => x.Name);
        return departments;
    }
}

