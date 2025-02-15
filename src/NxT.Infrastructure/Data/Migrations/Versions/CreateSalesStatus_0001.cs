using FluentMigrator;

namespace NxT.Infrastructure.Data.Migrations.Versions;

[Migration(Snapshot.SaleStatusTable, "create sale status table to manage sale progress")]
public sealed class CreateSaleStatus_0001 : Migration
{
    public override void Up()
    {
        Create.Table("SaleStatus")
            .WithColumn("ID").AsInt32().PrimaryKey().Identity()
            .WithColumn("Situation").AsString(100).NotNullable();

        Insert.IntoTable("SaleStatus")
            .Row(new { Situation = "Pending" })
            .Row(new { Situation = "Billed" })
            .Row(new { Situation = "Canceled" });
        
        Create.ForeignKey("FK_SalesRecord_SaleStatus")
            .FromTable("SalesRecords").ForeignColumn("Status")
            .ToTable("SaleStatus").PrimaryColumn("ID");
    }

    public override void Down()
    {
        Delete.ForeignKey("FK_SalesRecord_SaleStatus");
        Delete.Table("SaleStatus");
    }
}