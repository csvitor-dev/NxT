using NxT.Core.Models;

namespace NxT.Core.ViewModels;

public class SellerForm(ICollection<Department> departments)
{
    public Seller Seller { get; set;  } = null!;
    public ICollection<Department> Departments { get; } = departments;
}
