namespace SalesWebMvc.Services;
public interface IDepartmentsService
{
    Task<List<DepartmentViewModel>> DepartmentsToListAsync();
    Task<bool> InsertDepartmentAsync(DepartmentViewModel model);
    Task<DepartmentViewModel> FindDepartmentAsync(int? id);
    Task<bool> EditDepartmentAsync(DepartmentViewModel model);
    Task<bool> DeleteDepartmentAsync(int? id);
    Task<bool> IsDepartmentRegisteredAsync(DepartmentViewModel model);
}

