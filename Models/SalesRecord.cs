using SalesApp.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace SalesApp.Models
{
    public class SalesRecord
    {
        public int ID { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Date { get; set; }

        [DisplayFormat(DataFormatString = "{0:C2}")]
        public double Amount { get; set; }
        public ESaleStatus Status { get; set; }
        public int SellerID { get; set; }
        public Seller Seller { get; set; } = null!;

        public SalesRecord() { }
        public SalesRecord(int id, DateTime date, double amount, ESaleStatus status, Seller seller)
        {
            ID = id;
            Date = date;
            Amount = amount;
            Status = status;
            Seller = seller;
            SellerID = seller.ID;
        }
    }
}
