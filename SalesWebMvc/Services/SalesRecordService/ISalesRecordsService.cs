namespace SalesWebMvc.Services;
public interface ISalesRecordsService
{
    Task<List<SalesRecordViewModel>> GetAllRecordsAsync();
    Task<List<SalesRecordViewModel>> FindByDateAsync(DateTime? minDate, DateTime? maxDate);
}

