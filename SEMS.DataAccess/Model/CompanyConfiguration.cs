// ReSharper disable RedundantUsingDirective
// ReSharper disable DoNotCallOverridableMethodsInConstructor
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable PartialMethodWithSinglePart
// ReSharper disable RedundantNameQualifier
// TargetFrameworkVersion = 4.51
#pragma warning disable 1591    //  Ignore "Missing XML Comment" warning

using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Threading;
using System.Threading.Tasks;
using DatabaseGeneratedOption = System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption;

namespace SEMS.DataAccess.Model
{
    internal partial class CompanyConfiguration : EntityTypeConfiguration<Company>
    {
        public CompanyConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".Company");
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName("Id").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.CompanyName).HasColumnName("CompanyName").IsRequired().HasMaxLength(50);
            Property(x => x.ParentId).HasColumnName("ParentId").IsOptional();
            Property(x => x.Remark).HasColumnName("Remark").IsOptional().HasMaxLength(200);
            Property(x => x.CreateBy).HasColumnName("CreateBy").IsOptional();
            Property(x => x.CreateDate).HasColumnName("CreateDate").IsOptional();
            Property(x => x.ModifyBy).HasColumnName("ModifyBy").IsOptional();
            Property(x => x.ModifyDate).HasColumnName("ModifyDate").IsOptional();
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
