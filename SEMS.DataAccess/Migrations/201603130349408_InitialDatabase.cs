namespace SEMS.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Base_Company",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CompanyName = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                        ParentId = c.Int(),
                        CreateBy = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false, precision: 0),
                        ModifyBy = c.Int(),
                        ModifyDate = c.DateTime(precision: 0),
                        Remark = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Base_Department",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DepartmentNo = c.String(unicode: false),
                        DepartmentName = c.String(unicode: false),
                        CreateBy = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false, precision: 0),
                        ModifyBy = c.Int(),
                        ModifyDate = c.DateTime(precision: 0),
                        Remark = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Base_Department");
            DropTable("dbo.Base_Company");
        }
    }
}
