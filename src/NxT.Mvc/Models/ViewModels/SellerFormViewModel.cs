namespace NxT.Mvc.Models.ViewModels
{
    public class SellerFormViewModel(ICollection<Department> departments)
    {
        public Seller Seller { get; set;  } = null!;
        public ICollection<Department> Departments { get; } = departments;
    }
}
