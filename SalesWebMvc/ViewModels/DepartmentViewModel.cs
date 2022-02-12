#nullable disable
namespace SalesWebMvc.ViewModels;
public class DepartmentViewModel 
{
    public int Id { get; set; }

    [Required(ErrorMessage = "{0} required")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "{0} size should be between {2} and {1}")]
    public string Name { get; set; }
}

