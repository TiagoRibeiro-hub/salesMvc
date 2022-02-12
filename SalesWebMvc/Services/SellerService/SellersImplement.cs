namespace SalesWebMvc.Services;
internal class SellersImplement : ISellersService
{
    private readonly SalesDbContext _db;
    private readonly ErrorViewModel _errorViewModel;
    public SellersImplement(SalesDbContext db, ErrorViewModel errorViewModel)
    {
        _db = db;
        _errorViewModel = errorViewModel;
    }
    internal (bool, Seller) GetSellersIfExistsById(SellerViewModel model)
    {
        Seller seller = new Seller()
        {
            Id = model.Id,
            Name = model.Name,
            Email = model.Email,
            BirthDate = model.BirthDate,
            BaseSalary = model.BaseSalary,
            DepartmentId = model.DepartmentId,
        };
        bool exist = _db.Seller.Any(x => x.Id == seller.Id);
        return (exist, seller);
    }
    internal async Task<bool> Save()
    {
        var res = await _db.SaveChangesAsync();
        if (res > 0)
        {
            return true;
        }
        return false;
    }
    public async Task<List<SellerViewModel>> SellersToListAsync()
    {
        var res = await _db.Seller.ToListAsync();
        List<SellerViewModel> list = new();
        foreach (var seller in res)
        {
            SellerViewModel viewModel = new SellerViewModel();
            viewModel.Id = seller.Id;
            viewModel.Name = seller.Name;
            viewModel.Email = seller.Email;
            viewModel.BirthDate = seller.BirthDate;
            viewModel.BaseSalary = seller.BaseSalary;
            viewModel.DepartmentId = seller.DepartmentId;
            list.Add(viewModel);
        }
        return list;
    }
    public async Task<bool> IsSellerRegisteredAsync(SellerViewModel model)
    {
        return await _db.Seller.AnyAsync(x => x.Name == model.Name && x.Email == model.Email && x.BirthDate == model.BirthDate);
    }
    public async Task<bool> InsertSellerAsync(SellerViewModel model)
    {
        Seller seller = new()
        {
            Name = model.Name,
            Email = model.Email,
            BirthDate = model.BirthDate,
            BaseSalary = model.BaseSalary,
            DepartmentId = model.DepartmentId,
        };
            await _db.Seller.AddAsync(seller);
            return await Save();
    }
    public async Task<SellerViewModel> FindSellerAsync(int? id)
    {
        var res = await _db.Seller.FirstOrDefaultAsync(x => x.Id == id);
        return new SellerViewModel()
        {
            Id = res!.Id,
            Name = res.Name,
            Email = res.Email,
            BirthDate = res.BirthDate,
            BaseSalary = res.BaseSalary,
            DepartmentId = res.DepartmentId,
        };
    }
    public async Task<bool> EditSellerAsync(SellerViewModel model)
    {
        var res = GetSellersIfExistsById(model);
        if (res.Item1)
        {
            _db.Seller.Update(res.Item2);
            return await Save();
        }
        return false;
    }
    public async Task<bool> DeleteSellerAsync(int? id)
    {
        string sellerName = string.Empty;
        try
        {
            var seller = await _db.Seller.FindAsync(id);
            if(seller is not null)
            {
                sellerName = seller.Name;
                _db.Seller.Remove(seller!);
                return await Save();
            }
            return false;
        }
        catch (DbUpdateException ex)
        {
            throw new IntegrityException(_errorViewModel.CanNotDeleteSeller(sellerName));
        }
        
    }
    public async Task<SellerViewModel> GetSellerAsync(int? id)
    {
        var seller = await _db.Seller.Include(x => x.Department).FirstOrDefaultAsync(x => x.Id == id);
        return new SellerViewModel()
        {
            Id = seller!.Id,
            Name = seller.Name,
            Email = seller.Email,
            BirthDate = seller.BirthDate,
            BaseSalary = seller.BaseSalary,
            DepartmentId = seller.DepartmentId,
            DepartmentName = seller.Department.Name
        };
    }
}

