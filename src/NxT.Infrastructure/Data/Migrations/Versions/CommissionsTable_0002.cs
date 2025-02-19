using FluentMigrator;

namespace NxT.Infrastructure.Data.Migrations.Versions;

[Migration(Snapshot.CreateCommissionsTable, "create 'commissions' complementary entity concept")]
public sealed class CommissionsTable_0002 : Migration
{
    public override void Up()
    {
        // pivot table
        Create.Table("Commissions")
            .WithColumn("ID").AsInt32().PrimaryKey();

        Create.Table("CommissionPerSale")
            .WithColumn("ID").AsInt32().PrimaryKey()
            .WithColumn("BaseRate").AsDecimal(18, 2).NotNullable();
        
        Create.ForeignKey("FK_PerSale_Commission")
            .FromTable("CommissionPerSale").ForeignColumn("ID")
            .ToTable("Commissions").PrimaryColumn("ID");
        
        Create.Table("CommissionPerGoal")
            .WithColumn("ID").AsInt32().PrimaryKey()
            .WithColumn("BaseRate").AsDecimal(18, 2).NotNullable()
            .WithColumn("Goal").AsDecimal(18, 2).NotNullable()
            .WithColumn("ExtraRate").AsDecimal(18, 2).NotNullable();

        Create.ForeignKey("FK_PerGoal_Commission")
            .FromTable("CommissionPerGoal").ForeignColumn("ID")
            .ToTable("Commissions").PrimaryColumn("ID");

        Create.Table("TieredCommission")
            .WithColumn("ID").AsInt32().PrimaryKey();

        Create.ForeignKey("FK_Tiered_Commission")
            .FromTable("TieredCommission").ForeignColumn("ID")
            .ToTable("Commissions").PrimaryColumn("ID");

        Create.Table("Range")
            .WithColumn("ID").AsInt32().PrimaryKey().Identity()
            .WithColumn("Amount").AsDecimal(18, 2).NotNullable()
            .WithColumn("CommissionRate").AsDecimal(18, 2).NotNullable()
            .WithColumn("TierID").AsInt32().NotNullable();
        
        Create.ForeignKey("FK_Range_Tiered")
            .FromTable("Range").ForeignColumn("TierID")
            .ToTable("TieredCommission").PrimaryColumn("ID");

        Alter.Table("Sellers")
            .AddColumn("CommissionID").AsInt32().Nullable();

        Create.ForeignKey("FK_Sellers_Commission")
            .FromTable("Sellers").ForeignColumn("CommissionID")
            .ToTable("Commissions").PrimaryColumn("ID");
    }

    public override void Down()
    {
        Delete.ForeignKey("FK_PerSale_Commission").OnTable("CommissionPerSale");
        Delete.ForeignKey("FK_PerGoal_Commission").OnTable("CommissionPerGoal");
        Delete.ForeignKey("FK_Tiered_Commission").OnTable("TieredCommission");
        Delete.ForeignKey("FK_Range_Tiered").OnTable("Range");
        Delete.ForeignKey("FK_Sellers_Commission").OnTable("Sellers");

        Delete.Column("CommissionID")
            .FromTable("Sellers");

        Delete.Table("Range");
        Delete.Table("TieredCommission");
        Delete.Table("CommissionPerGoal");
        Delete.Table("CommissionPerSale");
        Delete.Table("Commissions");
    }
}