#nullable disable
namespace SalesWebMvc.Data;
public class Seller
{
    public Seller()
    {

    }
    public Seller(
        string name, string email, 
        DateTime birthDate, double baseSalary, 
        Department department)
    {
        Name = name;
        Email = email;
        BirthDate = birthDate;
        BaseSalary = baseSalary;
        Department = department;
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public DateTime BirthDate { get; set; }
    public double BaseSalary { get; set; }
    public Department Department { get; set; }
    public int DepartmentId { get; set; }
    public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>();

    public double TotalSales(DateTime initial, DateTime final)
    {
        return Sales.Where(x => x.Date >= initial && x.Date <= final).Sum(x => x.Amount);
    }

}

