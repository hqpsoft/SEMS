using SEMS.DataAccess.Model.Organization;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEMS.DataAccess.FluentAPI
{
    public class DepartmentMap : EntityTypeConfiguration<DepartmentModel>
    {
        public DepartmentMap()
        {
            ToTable("Base_Department");
        }
    }
}
