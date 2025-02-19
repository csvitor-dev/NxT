using NxT.Core.Contracts;
using NxT.Core.Models;
using NxT.Core.Models.Enums;

namespace NxT.Test.Base;

[TestFixture]
public class CalculateCommissionTest
{
    [Test]
    public void Test_NoCommission()
    {
        // Arrange
        var seller = new Seller
        {
            ID = 1,
            Name = "David Mills",
            Email = "mills.seller@nxt.sales.com",
            BirthDate = new DateTime(1995, 12, 15),
            BaseSalary = 3500.0m,
        };
        var record = new SalesRecord
        {
            ID = 1,
            Amount = 250.0m,
            Date = DateTime.Now,
            Seller = seller,
            Status = ESaleStatus.Billed,
        };


        // Act
        seller.AddSales(record);
        var total = seller.CalculateCommission();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(seller.Sales, Is.Not.Empty);
            Assert.That(total, Is.EqualTo(0.0m));
        });
    }

    [Test]
    public void Test_CommissionPerSale_WithOneSale()
    {
        var seller = new Seller
        {
            ID = 2,
            Name = "John Doe",
            Email = "john-doe.seller@nxt.sales.com",
            BirthDate = new DateTime(1995, 12, 15),
            BaseSalary = 1050.0m,
            Commission = new CommissionPerSale(1, 0.2m),
        };
        var record = new SalesRecord
        {
            ID = 1,
            Amount = 350.0m,
            Status = ESaleStatus.Billed,
            Date = DateTime.Now,
            Seller = seller,
        };

        seller.AddSales(record);
        var total = seller.CalculateCommission();

        Assert.Multiple(() =>
        {
            Assert.That(seller.Sales, Is.Not.Empty);
            Assert.That(total, Is.EqualTo(0.2m * 350.0m));
        });
    }

    [Test]
    public void Test_CommissionPerSale_WithTwoSales()
    {
        var seller = new Seller
        {
            ID = 2,
            Name = "John Doe",
            Email = "john-doe.seller@nxt.sales.com",
            BirthDate = new DateTime(1995, 12, 15),
            BaseSalary = 1050.0m,
            Commission = new CommissionPerSale(1, 0.2m),
        };
        var record1 = new SalesRecord
        {
            ID = 1,
            Amount = 140.0m,
            Status = ESaleStatus.Billed,
            Date = DateTime.Now,
            Seller = seller,
        };
        var record2 = new SalesRecord
        {
            ID = 2,
            Amount = 220.0m,
            Status = ESaleStatus.Billed,
            Date = DateTime.Now.AddDays(-1),
            Seller = seller,
        };

        seller.AddSales(record1);
        seller.AddSales(record2);
        var total = seller.CalculateCommission();

        Assert.Multiple(() =>
        {
            Assert.That(seller.Sales, Is.Not.Empty);
            Assert.That(total, Is.EqualTo(0.2m * (140.0m + 220.0m)));
        });
    }

    [Test]
    public void Test_CommissionPerGoal_WhenNotAchieved()
    {
        // Arrange
        var seller = new Seller
        {
            ID = 3,
            Name = "Mark Harrison",
            Email = "mark-harr.seller@nxt.sales.com",
            BirthDate = new DateTime(2003, 10, 17),
            BaseSalary = 550.0m,
            Commission = new CommissionPerGoal(2, 2500.0m, 0.15m, 0.13m),
        };
        var record = new SalesRecord
        {
            ID = 1,
            Amount = 140.0m,
            Status = ESaleStatus.Billed,
            Date = DateTime.Now,
            Seller = seller,
        };

        seller.AddSales(record);
        var total = seller.CalculateCommission();

        Assert.Multiple(() =>
        {
            Assert.That(seller.Sales, Is.Not.Empty);
            Assert.That(total, Is.EqualTo(0.0m));
        });
    }
    
    [Test]
    public void Test_CommissionPerGoal_WhenAchieved()
    {
        // Arrange
        var seller = new Seller
        {
            ID = 3,
            Name = "Mark Harrison",
            Email = "mark-harr.seller@nxt.sales.com",
            BirthDate = new DateTime(2003, 10, 17),
            BaseSalary = 550.0m,
            Commission = new CommissionPerGoal(2, 2500.0m, 0.15m, 0.13m),
        };
        var record1 = new SalesRecord
        {
            ID = 1,
            Amount = 140.0m,
            Status = ESaleStatus.Billed,
            Date = DateTime.Now,
            Seller = seller,
        };
        var record2 = new SalesRecord
        {
            ID = 2,
            Amount = 2450.0m,
            Status = ESaleStatus.Billed,
            Date = DateTime.Now.AddDays(-1),
            Seller = seller,
        };

        seller.AddSales(record1);
        seller.AddSales(record2);
        var total = seller.CalculateCommission();

        Assert.Multiple(() =>
        {
            Assert.That(seller.Sales, Is.Not.Empty);
            Assert.That(total, Is.EqualTo(2500.0m * 0.15m + 90.0m * 0.13m));
        });
    }

    [Test]
    public void Test_TieredCommission_OnTierOne()
    {
        var ranges = new List<TierRange>
        {
            new(1, 10_000m, 0.1m, 3),
            new(2, 13_000m, 0.15m, 3)
        };
        var seller = new Seller
        {
            ID = 4,
            Name = "Washington Elliot",
            Email = "eli.seller@nxt.sales.com",
            BirthDate = new DateTime(2010, 1, 15),
            BaseSalary = 1000.0m,
            Commission = new TieredCommission(3, ranges),
        };
        var record1 = new SalesRecord
        {
            ID = 1,
            Amount = 80.0m,
            Status = ESaleStatus.Billed,
            Date = DateTime.Now,
            Seller = seller,
        };
        var record2 = new SalesRecord
        {
            ID = 2,
            Amount = 590.5m,
            Status = ESaleStatus.Billed,
            Date = DateTime.Now.AddDays(-1),
            Seller = seller,
        };

        seller.AddSales(record1);
        seller.AddSales(record2);
        var total = seller.CalculateCommission();

        Assert.Multiple(() =>
        {
            Assert.That(seller.Sales, Is.Not.Empty);
            Assert.That(total, Is.EqualTo(0.1m * (80.0m + 590.5m)));
        });
    }
    
    [Test]
    public void Test_TieredCommission_OnTierTwo()
    {
        var ranges = new List<TierRange>
        {
            new(1, 10_000m, 0.1m, 3),
            new(2, 13_000m, 0.15m, 3)
        };
        var seller = new Seller
        {
            ID = 5,
            Name = "William Somerset",
            Email = "somerset.seller@nxt.sales.com",
            BirthDate = new DateTime(1995, 12, 15),
            BaseSalary = 1000.0m,
            Commission = new TieredCommission(3, ranges),
        };
        var record1 = new SalesRecord
        {
            ID = 1,
            Amount = 9000.0m,
            Status = ESaleStatus.Billed,
            Date = DateTime.Now,
            Seller = seller,
        };
        var record2 = new SalesRecord
        {
            ID = 2,
            Amount = 5575.80m,
            Status = ESaleStatus.Billed,
            Date = DateTime.Now.AddDays(-1),
            Seller = seller,
        };
        var record3 = new SalesRecord
        {
            ID = 3,
            Amount = 8424.20m,
            Status = ESaleStatus.Billed,
            Date = DateTime.Now.AddDays(-2),
            Seller = seller,
        };


        seller.AddSales(record1);
        seller.AddSales(record2);
        seller.AddSales(record3);
        var total = seller.CalculateCommission();

        Assert.Multiple(() =>
        {
            Assert.That(seller.Sales, Is.Not.Empty);
            Assert.That(total, Is.EqualTo(0.1m * (8480.20m + 1519.8m) + 0.15m * (9000.0m + 4000.0m)));
        });
    }
}