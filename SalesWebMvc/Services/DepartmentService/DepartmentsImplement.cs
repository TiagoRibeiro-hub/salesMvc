namespace SalesWebMvc.Services;
internal class DepartmentsImplement : IDepartmentsService
{
    private readonly SalesDbContext _db;

    public DepartmentsImplement(SalesDbContext db)
    {
        _db = db;
    }
    internal (bool, Department) GetDepartmentIfExists(DepartmentViewModel model)
    {
        Department department = new ()
        {
            Id = model.Id,
            Name = model.Name
        };
        bool exist = _db.Department.Any(x => x.Id == department.Id);
        return (exist, department);
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
    public async Task<List<DepartmentViewModel>> DepartmentsToListAsync()
    {
        var res = await _db.Department.ToListAsync();
        List<DepartmentViewModel> list = new();
        foreach (var department in res)
        {
            DepartmentViewModel viewModel = new ();
            viewModel.Id = department.Id;
            viewModel.Name = department.Name;
            list.Add(viewModel);
        }
        return list;
    }

    public async Task<bool> IsDepartmentRegisteredAsync(DepartmentViewModel model)
    {
        return await _db.Department.AnyAsync(x => x.Name == model.Name);
    }
    public async Task<bool> InsertDepartmentAsync(DepartmentViewModel model)
    {
        Department department = new()
        {
            Name = model.Name
        };
        await _db.Department.AddAsync(department);
        return await Save();
    }

    public async Task<DepartmentViewModel> FindDepartmentAsync(int? id)
    {
        var res = await _db.Department.FirstOrDefaultAsync(x => x.Id == id);
        return new DepartmentViewModel()
        {
            Id = res!.Id,
            Name = res.Name
        };
    }

    public async Task<bool> EditDepartmentAsync(DepartmentViewModel model)
    {
        var res = GetDepartmentIfExists(model);
        if (res.Item1)
        {
            _db.Department.Update(res.Item2);
            return await Save();
        }
        return false;
    }

    public async Task<bool> DeleteDepartmentAsync(int? id)
    {
        var department = await _db.Department.FindAsync(id);
        _db.Department.Remove(department!);
        return await Save();
    }
}

