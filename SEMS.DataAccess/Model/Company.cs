
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




    public partial class Company
    {




        public Guid Id { get; set; }



        ///<summary>
        /// 公司名称
        ///</summary>


        public string CompanyName { get; set; }



        ///<summary>
        /// 父级Id
        ///</summary>


        public Guid? ParentId { get; set; }



        public Guid? CreateBy { get; set; }



        public DateTime? CreateDate { get; set; }



        public Guid? ModifyBy { get; set; }




    }




}
