using SalesApp.Models.Enums;

namespace SalesApp.Data
{
    public class SeedingService(SalesWebMvcContext context)
    {
        private SalesWebMvcContext _context = context;

        public void Seed()
        {
            if (_context.Department.Any() || _context.Seller.Any() || _context.SalesRecord.Any())
                return; // DB has been seeded

            Department departmentOne = new(1, "Computers");
            Department departmentTwo = new(2, "Eletronics");
            Department departmentThree = new(3, "Fashion");
            Department departmentFour = new(4, "Books");

            Seller sellerOne = new(1, "Bob Brown", "bob@gmail.com", new(1998, 4, 21), 1000.0, departmentOne);
            Seller sellerTwo = new(2, "Maria Green", "maria@gmail.com", new(1979, 12, 31), 3500.0, departmentTwo);
            Seller sellerThree = new(3, "Alex Grey", "alex@gmail.com", new(1988, 1, 15), 2200.0, departmentOne);
            Seller sellerFour = new(4, "Martha Red", "martha@gmail.com", new(1993, 11, 30), 3000.0, departmentFour);
            Seller sellerFive = new(5, "Donald Blue", "donald@gmail.com", new(2000, 1, 9), 4000.0, departmentThree);
            Seller sellerSix = new(6, "Alex Pink", "bob@gmail.com", new(1997, 3, 4), 3000.0, departmentTwo);

            SalesRecord recordOne = new(1, new(2018, 09, 25), 11000.0, ESaleStatus.Billed, sellerOne);
            SalesRecord recordTwo = new(2, new(2018, 09, 4), 7000.0, ESaleStatus.Billed, sellerFive);
            SalesRecord recordThree = new(3, new(2018, 09, 13), 4000.0, ESaleStatus.Canceled, sellerFour);
            SalesRecord recordFour = new(4, new(2018, 09, 1), 8000.0, ESaleStatus.Billed, sellerOne);
            SalesRecord recordFive = new(5, new(2018, 09, 21), 3000.0, ESaleStatus.Billed, sellerThree);
            SalesRecord recordSix = new(6, new(2018, 09, 15), 2000.0, ESaleStatus.Billed, sellerOne);
            SalesRecord recordSeven = new(7, new(2018, 09, 28), 13000.0, ESaleStatus.Billed, sellerTwo);
            SalesRecord recordEight = new(8, new(2018, 09, 11), 4000.0, ESaleStatus.Billed, sellerFour);
            SalesRecord recordNine = new(9, new(2018, 09, 14), 11000.0, ESaleStatus.Pending, sellerSix);
            SalesRecord recordTen = new(10, new(2018, 09, 7), 9000.0, ESaleStatus.Billed, sellerSix);
            SalesRecord recordEleven = new(11, new(2018, 09, 13), 6000.0, ESaleStatus.Billed, sellerTwo);
            SalesRecord recordTwelve = new(12, new(2018, 09, 25), 7000.0, ESaleStatus.Pending, sellerThree);
            SalesRecord recordThirteen = new(13, new(2018, 09, 29), 10000.0, ESaleStatus.Billed, sellerFour);
            SalesRecord recordFourteen = new(14, new(2018, 09, 4), 3000.0, ESaleStatus.Billed, sellerFive);
            SalesRecord recordFifteen = new(15, new(2018, 09, 12), 4000.0, ESaleStatus.Billed, sellerOne);
            SalesRecord recordSixteen = new(16, new(2018, 10, 5), 2000.0, ESaleStatus.Billed, sellerFour);
            SalesRecord recordSeventeen = new(17, new(2018, 10, 1), 12000.0, ESaleStatus.Billed, sellerOne);
            SalesRecord recordEighteen = new(18, new(2018, 10, 24), 6000.0, ESaleStatus.Billed, sellerThree);
            SalesRecord recordNineteen = new(19, new(2018, 10, 22), 8000.0, ESaleStatus.Billed, sellerFive);
            SalesRecord recordTwenty = new(20, new(2018, 10, 15), 8000.0, ESaleStatus.Billed, sellerSix);
            SalesRecord recordTwentyOne = new(21, new(2018, 10, 17), 9000.0, ESaleStatus.Billed, sellerTwo);
            SalesRecord recordTwentyTwo = new(22, new(2018, 10, 24), 4000.0, ESaleStatus.Billed, sellerFour);
            SalesRecord recordTwentyThree = new(23, new(2018, 10, 19), 11000.0, ESaleStatus.Canceled, sellerTwo);
            SalesRecord recordTwentyFour = new(24, new(2018, 10, 12), 8000.0, ESaleStatus.Billed, sellerFive);
            SalesRecord recordTwentyFive = new(25, new(2018, 10, 31), 7000.0, ESaleStatus.Billed, sellerThree);
            SalesRecord recordTwentySix = new(26, new(2018, 10, 6), 5000.0, ESaleStatus.Billed, sellerFour);
            SalesRecord recordTwentySeven = new(27, new(2018, 10, 13), 9000.0, ESaleStatus.Pending, sellerOne);
            SalesRecord recordTwentyEight = new(28, new(2018, 10, 7), 4000.0, ESaleStatus.Billed, sellerThree);
            SalesRecord recordTwentyNine = new(29, new(2018, 10, 23), 12000.0, ESaleStatus.Billed, sellerFive);
            SalesRecord recordThirty = new(30, new(2018, 10, 12), 5000.0, ESaleStatus.Billed, sellerTwo);

            _context.Department
                .AddRange(departmentOne, departmentTwo, departmentThree, departmentFour);

            _context.Seller
                .AddRange(sellerOne, sellerTwo, sellerThree, sellerFour, sellerFive, sellerSix);

            _context.SalesRecord.AddRange(
                recordOne, recordTwo, recordThree, recordFour, recordFive, recordSix, recordSeven, recordEight, recordNine, recordTen,
                recordEleven, recordTwelve, recordThirteen, recordFourteen, recordFifteen, recordSixteen, recordSeventeen, recordEighteen,
                recordNineteen, recordTwenty, recordTwentyOne, recordTwentyTwo, recordTwentyThree, recordTwentyFour,recordTwentyFive,
                recordTwentySix, recordTwentySeven, recordTwentyEight, recordTwentyNine, recordThirty
            );

            _context.SaveChanges();
        }
    }
}
