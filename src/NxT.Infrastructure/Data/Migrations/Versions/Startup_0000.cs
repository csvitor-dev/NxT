using FluentMigrator;

namespace NxT.Infrastructure.Data.Migrations.Versions;

[Migration(Snapshot.Startup, "create base tables for the application")]
public sealed class Startup_0000 : Migration
{
    public override void Up()
    {
        Create.Table("Departments")
            .WithColumn("ID").AsInt32().PrimaryKey().Identity()
            .WithColumn("Name").AsString(100).NotNullable();

        Create.Table("Sellers")
            .WithColumn("ID").AsInt32().PrimaryKey().Identity()
            .WithColumn("Name").AsString(100).NotNullable()
            .WithColumn("Email").AsString(100).NotNullable()
            .WithColumn("BirthDate").AsDateTime().NotNullable()
            .WithColumn("BaseSalary").AsDecimal().NotNullable()
            .WithColumn("DepartmentID").AsInt32().NotNullable();

        Create.ForeignKey("FK_Seller_Department")
            .FromTable("Sellers").ForeignColumn("DepartmentID")
            .ToTable("Departments").PrimaryColumn("ID")
            .OnDelete(System.Data.Rule.Cascade);

        Create.Table("SalesRecords")
            .WithColumn("ID").AsInt32().PrimaryKey().Identity()
            .WithColumn("Date").AsDateTime().NotNullable()
            .WithColumn("Amount").AsDecimal().NotNullable()
            .WithColumn("Status").AsInt32().NotNullable()
            .WithColumn("SellerID").AsInt32().NotNullable();

        Create.ForeignKey("FK_SalesRecord_Seller")
            .FromTable("SalesRecords").ForeignColumn("SellerID")
            .ToTable("Sellers").PrimaryColumn("ID")
            .OnDelete(System.Data.Rule.Cascade);
    }

    public override void Down()
    {
        Delete.Table("Departments");
        Delete.Table("Sellers");
        Delete.Table("SalesRecords");
    }
}