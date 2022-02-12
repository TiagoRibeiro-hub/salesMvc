#nullable disable
namespace SalesWebMvc.Data;
public class Department
{
    public Department()
    {
    }

    public Department(string name)
    {
        Name = name;
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<Seller> Sellers { get; set; } = new List<Seller>();

    public double TotalSales(DateTime initial, DateTime final)
    {
        return Sellers.Sum(seller => seller.TotalSales(initial, final));
    }
}


