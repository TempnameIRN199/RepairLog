namespace RepairLogServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class firstMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Breakdowns",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false),
                        Cause = c.String(nullable: false),
                        DeviceId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Devices", t => t.DeviceId, cascadeDelete: true)
                .Index(t => t.DeviceId);
            
            CreateTable(
                "dbo.Devices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DevName = c.String(nullable: false),
                        IsWorking = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Non_repairable",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DeviceId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Devices", t => t.DeviceId, cascadeDelete: true)
                .Index(t => t.DeviceId);
            
            CreateTable(
                "dbo.Repaireds",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DeviceId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Devices", t => t.DeviceId, cascadeDelete: true)
                .Index(t => t.DeviceId);
            
            CreateTable(
                "dbo.Repairs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DeviceId = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        Status = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Devices", t => t.DeviceId, cascadeDelete: true)
                .Index(t => t.DeviceId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Repairs", "DeviceId", "dbo.Devices");
            DropForeignKey("dbo.Repaireds", "DeviceId", "dbo.Devices");
            DropForeignKey("dbo.Non_repairable", "DeviceId", "dbo.Devices");
            DropForeignKey("dbo.Breakdowns", "DeviceId", "dbo.Devices");
            DropIndex("dbo.Repairs", new[] { "DeviceId" });
            DropIndex("dbo.Repaireds", new[] { "DeviceId" });
            DropIndex("dbo.Non_repairable", new[] { "DeviceId" });
            DropIndex("dbo.Breakdowns", new[] { "DeviceId" });
            DropTable("dbo.Repairs");
            DropTable("dbo.Repaireds");
            DropTable("dbo.Non_repairable");
            DropTable("dbo.Devices");
            DropTable("dbo.Breakdowns");
        }
    }
}
