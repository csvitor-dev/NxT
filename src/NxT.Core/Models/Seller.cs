using System.ComponentModel.DataAnnotations;

namespace NxT.Core.Models;
public class Seller
{
    public int ID { get; set; }

    [Required(ErrorMessage = "{0} Required")]
    [StringLength(60, MinimumLength = 7, ErrorMessage = "{0} size should be between {2} and {1}")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "{0} Required")]
    [EmailAddress(ErrorMessage ="Enter a valid email")]
    [DataType(DataType.EmailAddress)]
    public string? Email { get; set; }

    [Required(ErrorMessage = "{0} Required")]
    [Display(Name = "Birth Date")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
    public DateTime BirthDate { get; set; }

    [Required(ErrorMessage = "{0} Required")]
    [Range(100.0, 20000.0, ErrorMessage = "{0} must be from {1} to {2}")]
    [Display(Name = "Base Salary")]
    [DisplayFormat(DataFormatString = "{0:C2}")]
    public decimal BaseSalary { get; set; }

    public int DepartmentID { get; set; }
    public Department? Department { get; set; }
    public ICollection<SalesRecord> Sales { get; } = null!;

    public Seller() => Sales = [];
    public Seller(int id, string? name, string? email, DateTime birthDate, decimal baseSalary, Department department) : this()
    {
        ID = id;
        Name = name;
        Email = email;
        BirthDate = birthDate;
        BaseSalary = baseSalary;

        Department = department;
        DepartmentID = department.ID;
    }

    public void AddSales(SalesRecord record) 
        => Sales.Add(record);
    public void RemoveSales(SalesRecord record) 
        => Sales.Remove(record);

    public decimal TotalSales(DateTime initial, DateTime final)
        => (from record in Sales 
            where record.Date >= initial && record.Date <= final
            select record.Amount).Sum();
}
