using EmitMapper;
using Mehdime.Entity;
using SEMS.Abstracts;
using SEMS.DataAccess.Dto.Base;
using SEMS.DataAccess.Model;
using SEMS.DataAccess.Model.Organization;
using SEMS.Infrastructure.Data;
using SEMS.Infrastructure.Exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEMS.Concretes
{
    public class DepartmentSvc : IDepartmentSvc
    {
        private readonly IDbContextScopeFactory _dbScopeFactory;

        public DepartmentSvc(IDbContextScopeFactory dbScopeFactory)
        {
            _dbScopeFactory = dbScopeFactory;
        }

        public void CreatDepartment(DepartmentDto dto)
        {
            try
            {
                var entity = ObjectMapperManager.DefaultInstance.GetMapper<DepartmentDto, DepartmentModel>().Map(dto);
                entity.CreateBy = 0;
                entity.CreateDate = DateTime.Now;
                using (var dbScope = _dbScopeFactory.Create())
                {
                    var db = dbScope.DbContexts.Get<SEMSDbContext>();
                    var data = db.Departments.Add(entity);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new TipInfoException(ex.Message);
            }
        }

        public PageGridData<DepartmentDto> GetDepartmentPage(DataAccess.Query.DepartmentQuery query)
        {
            using (var dbScope = _dbScopeFactory.CreateReadOnly())
            {
                var db = dbScope.DbContexts.Get<SEMSDbContext>();
                var data = db.Companies.Select(r => new DepartmentDto
                {
                    Name = r.CompanyName,
                    Remark = r.Remark,
                    Id = r.Id,
                });
                if (!string.IsNullOrEmpty(query.Search))
                {
                    data = data.Where(r => r.Name.Contains(query.Search));
                }
                var result = data.OrderBy(r => r.Id).Skip(query.Offset).Take(query.Limit).ToList();
                var total = data.Count();
                return new PageGridData<DepartmentDto> { rows = result, total = total };
            }
        }
    }
}
