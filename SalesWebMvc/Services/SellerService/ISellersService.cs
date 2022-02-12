namespace SalesWebMvc.Services;
public interface ISellersService
{
    Task<List<SellerViewModel>> SellersToListAsync();
    Task<bool> InsertSellerAsync(SellerViewModel model);
    Task<SellerViewModel> FindSellerAsync(int? id);
    Task<bool> EditSellerAsync(SellerViewModel model);
    Task<bool> DeleteSellerAsync(int? id);
    Task<bool> IsSellerRegisteredAsync(SellerViewModel model);
    Task<SellerViewModel> GetSellerAsync(int? id);
}

