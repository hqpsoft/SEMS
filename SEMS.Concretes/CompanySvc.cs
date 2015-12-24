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
using SEMS.DataAccess.Organization.Model;
using SEMS.DataAccess.Extensions;
using AutoMapper;
using SEMS.DataAccess.Dto.Base;

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
                var entity = Mapper.Map<CompanyDto, Company>(dto);
                entity.CreateBy = 0;
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

        public void DeleteCompany(int companyId)
        {
            try
            {
                using (var dbScope = _dbScopeFactory.Create())
                {
                    var db = dbScope.DbContexts.Get<SEMSDbContext>();
                    Company entity = new Company() { Id = companyId };
                    db.Companies.Attach(entity);
                    db.Companies.Remove(entity);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new TipInfoException(ex.Message);
            }
        }

        public void EditCompany(CompanyDto dto)
        {
            try
            {
                var entity = Mapper.Map<CompanyDto, Company>(dto);
                entity.ModifyBy = 0;
                entity.ModifyDate = DateTime.Now;
                using (var dbScope = _dbScopeFactory.Create())
                {
                    var db = dbScope.DbContexts.Get<SEMSDbContext>();
                    db.Update(entity, r => new { r.CompanyName, r.ModifyBy, r.ModifyDate, r.Remark, r.ParentId });
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new TipInfoException(ex.Message);
            }
        }

        public CompanyDto GetCompanyById(int companyId)
        {
            using (var dbScope = _dbScopeFactory.CreateReadOnly())
            {
                var db = dbScope.DbContexts.Get<SEMSDbContext>();
                var data = db.Companies.Where(r => r.Id == companyId).Select(r => new CompanyDto()
                {
                    Id = r.Id,
                    CompanyName = r.CompanyName,
                    Remark = r.Remark
                }).FirstOrDefault();

                return data;
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
                var result = data.OrderBy(r => r.Id).Skip(query.Offset).Take(query.Limit).ToList();
                var total = data.Count();
                return new PageGridData<CompanyDto> { rows = result, total = total };
            }
        }
    }
}
