namespace SalesWebMvc.Services;
internal class SalesRecordsImplement : ISalesRecordsService
{
    private readonly SalesDbContext _db;

    public SalesRecordsImplement(SalesDbContext db)
    {
        _db = db;
    }

    internal async Task<List<SalesRecordViewModel>> SalesRecordList(IQueryable<SalesRecord> records)
    {
        var recordsList = await records.Include(x => x.Seller).Include(x => x.Seller.Department)
            .OrderByDescending(x => x.Date).ToListAsync();

        List<SalesRecordViewModel> listViewModel = new();
        foreach (var item in recordsList)
        {
            SalesRecordViewModel viewModel = new();
            viewModel.Id = item.Id;
            viewModel.Date = item.Date;
            viewModel.Amount = item.Amount;
            viewModel.Status = item.Status;
            viewModel.Seller = item.Seller;
            listViewModel.Add(viewModel);
        }
        return listViewModel;
    }

    public async Task<List<SalesRecordViewModel>> GetAllRecordsAsync()
    {
        var records = from salesRecord in _db.SalesRecord select salesRecord;

        return await SalesRecordList(records);
    }

    public async Task<List<SalesRecordViewModel>> FindByDateAsync(DateTime? minDate, DateTime? maxDate)
    {
        var records = from salesRecord in _db.SalesRecord select salesRecord;
        if (minDate.HasValue)
        {
            records = records.Where(x => x.Date >= minDate.Value);
        }
        if (maxDate.HasValue)
        {
            records = records.Where(x => x.Date <= maxDate.Value);
        }
        var recordsList = await SalesRecordList(records);
        return recordsList;
    }
}

