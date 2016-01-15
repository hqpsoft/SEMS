namespace SEMS.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Base_Company",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CompanyName = c.String(nullable: false, maxLength: 50),
                        ParentId = c.Int(),
                        CreateBy = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        ModifyBy = c.Int(),
                        ModifyDate = c.DateTime(),
                        Remark = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Base_Department",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DepartmentNo = c.String(),
                        DepartmentName = c.String(),
                        CreateBy = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        ModifyBy = c.Int(),
                        ModifyDate = c.DateTime(),
                        Remark = c.String(),
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
