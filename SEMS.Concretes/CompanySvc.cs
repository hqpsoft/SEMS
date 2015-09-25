using Mehdime.Entity;
using SEMS.Abstracts;
using SEMS.DataAccess.Model;
using SEMS.Infrastructure.Exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEMS.Concretes
{
    public class CompanySvc : ICompanySvc
    {
        #region 构造函数注册上下文

        private readonly IDbContextScopeFactory _dbScopeFactory;

        public CompanySvc(IDbContextScopeFactory dbScopeFactory)
        {
            _dbScopeFactory = dbScopeFactory;
        }
        #endregion

        public void CreatCompany()
        {
            using (var dbScope = _dbScopeFactory.CreateReadOnly())
            {
                throw new TipInfoException("测试异常");
                var db = dbScope.DbContexts.Get<SEMSDbContext>();
                var data = db.Companies.ToList();
               
            }
        }
    }
}
