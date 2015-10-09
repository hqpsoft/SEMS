using Mehdime.Entity;
using SEMS.Abstracts;
using SEMS.DataAccess.Model;
using SEMS.Infrastructure.Exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SEMS.DataAccess.Dto;
using SEMS.DataAccess.Query;
using SEMS.Infrastructure.Data;
using EmitMapper;

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

        public void CreatCompany(CompanyDto dto)
        {
            try
            {
                var entity = ObjectMapperManager.DefaultInstance.GetMapper<CompanyDto, Company>().Map(dto);
                entity.Id = Guid.NewGuid();
                entity.CreateBy = Guid.NewGuid();
                entity.CreateDate = DateTime.Now;
                using (var dbScope = _dbScopeFactory.Create())
                {
                    var db = dbScope.DbContexts.Get<SEMSDbContext>();
                    var data = db.Companies.Add(entity);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new TipInfoException(ex.Message);
            }
        }

        public PageGridData<CompanyDto> GetCompanyPage(CompanyQuery query)
        {
            using (var dbScope = _dbScopeFactory.CreateReadOnly())
            {
                var db = dbScope.DbContexts.Get<SEMSDbContext>();
                var data = db.Companies.Select(r => new CompanyDto
                {
                    CompanyName = r.CompanyName,
                    Remark = r.Remark,
                    Id = r.Id,
                });
                if (!string.IsNullOrEmpty(query.Search))
                {
                    data = data.Where(r => r.CompanyName.Contains(query.Search));
                }
                var result = data.OrderBy(r => r.CompanyName).Skip(query.Offset).Take(query.Limit).ToList();
                var total = data.Count();
                return new PageGridData<CompanyDto> { rows = result, total = total };
            }
        }
    }
}
