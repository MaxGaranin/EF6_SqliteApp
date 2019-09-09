namespace ConsoleApp1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Flows",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FlowDirection = c.Int(nullable: false),
                        NetElementId = c.String(maxLength: 2147483647),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FlowStates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Period_LeftBorder = c.DateTime(nullable: false),
                        Period_RightBorder = c.DateTime(nullable: false),
                        IsMain = c.Boolean(nullable: false),
                        OilQm3PerDay = c.Double(),
                        WaterQm3PerDay = c.Double(),
                        GasQm3PerDay = c.Double(),
                        TemperatureCPerDay = c.Double(),
                        PressureAtmgPerDay = c.Double(),
                        ParkOilPerDay = c.Double(),
                        ParkLiquidPerDay = c.Double(),
                        ParkGasPerDay = c.Double(),
                        LiquidVelocityPerDay = c.Double(),
                        MinLiquidVelocityPerDay = c.Double(),
                        GasVelocityPerDay = c.Double(),
                        MinGasVelocityPerDay = c.Double(),
                        ErosionVelocity = c.Double(),
                        FluidVelocityPerDay = c.Double(),
                        MinFluidVelocityPerDay = c.Double(),
                        MinPressure = c.Double(),
                        MaxPressure = c.Double(),
                        OilMassTPerDay = c.Double(),
                        WaterMassTPerDay = c.Double(),
                        GasMassTPerDay = c.Double(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        FluidModel_Id = c.Guid(),
                        Flow_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FluidModels", t => t.FluidModel_Id)
                .ForeignKey("dbo.Flows", t => t.Flow_Id)
                .Index(t => t.FluidModel_Id)
                .Index(t => t.Flow_Id);
            
            CreateTable(
                "dbo.FluidModels",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(maxLength: 2147483647),
                        Description = c.String(maxLength: 2147483647),
                        OilSg = c.Double(nullable: false),
                        WaterSg = c.Double(nullable: false),
                        GasSg = c.Double(nullable: false),
                        OilHeatCapacity = c.Double(nullable: false),
                        WaterHeatCapacity = c.Double(nullable: false),
                        GasHeatCapacity = c.Double(nullable: false),
                        OilHeatConductivity = c.Double(nullable: false),
                        WaterHeatConductivity = c.Double(nullable: false),
                        GasHeatConductivity = c.Double(nullable: false),
                        OilFreezingTemperature = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FlowStates", "Flow_Id", "dbo.Flows");
            DropForeignKey("dbo.FlowStates", "FluidModel_Id", "dbo.FluidModels");
            DropIndex("dbo.FlowStates", new[] { "Flow_Id" });
            DropIndex("dbo.FlowStates", new[] { "FluidModel_Id" });
            DropTable("dbo.FluidModels");
            DropTable("dbo.FlowStates");
            DropTable("dbo.Flows");
        }
    }
}
