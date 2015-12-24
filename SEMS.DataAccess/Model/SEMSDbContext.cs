using SEMS.DataAccess.FluentAPI;
using SEMS.DataAccess.Model.Organization;
using SEMS.DataAccess.Organization.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEMS.DataAccess.Model
{
    public class SEMSDbContext: DbContext
    {
        #region DbSet 
        public DbSet<Company> Companies { get; set; }
        public DbSet<DepartmentModel> Departments { get; set; }
        #endregion


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ComapnyMap());
            modelBuilder.Configurations.Add(new DepartmentMap());
        }
        
    }
}
