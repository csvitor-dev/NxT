using System.ComponentModel.DataAnnotations;

namespace NxT.Core.Models;

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

    public void AddSeller(Seller seller) 
        => Sellers.Add(seller);
    
    public decimal TotalSales(DateTime initial, DateTime final)
        => (from seller in Sellers select seller.TotalSales(initial, final)).Sum();
}
