using SEMS.DataAccess.Organization.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEMS.DataAccess.FluentAPI
{
    public class ComapnyMap : EntityTypeConfiguration<Company>
    {
        public ComapnyMap()
        {
            //HasKey(r => r.Id);//指定主键
            Property(r => r.CompanyName).HasMaxLength(50).IsRequired();//长度50,必填
            ToTable("Base_Company");//指定生成表名
        }
    }
}
