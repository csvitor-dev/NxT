using System.ComponentModel.DataAnnotations;

namespace NxT.Mvc.Models
{
    public class Department
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [StringLength(60, MinimumLength = 5, ErrorMessage = "{0} size should be between {2} and {1}")]
        public string? Name { get; set; }
        public ICollection<Seller> Sellers { get; } = null!;

        public Department() => Sellers = [];
        public Department(int id, string name) : this()
        {
            ID = id;
            Name = name;
        }

        public void AddSeller(Seller seller) => Sellers.Add(seller);
        
        public double TotalSales(DateTime initial, DateTime final)
        {
            // double query = (from seller in Sellers select seller.TotalSales(initial, final)).Sum();
            double query = Sellers.Sum(seller => seller.TotalSales(initial, final));
            return query;
        }
    }
}
