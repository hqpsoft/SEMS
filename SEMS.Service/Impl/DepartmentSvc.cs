using AutoMapper;
using Mehdime.Entity;
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

namespace SEMS.Service.Impl
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
            var entity = Mapper.Map<DepartmentDto, Department>(dto);
            entity.CreateBy = 0;
            entity.CreateDate = DateTime.Now;
            using (var dbScope = _dbScopeFactory.Create())
            {
                var db = dbScope.DbContexts.Get<SEMSDbContext>();
                var data = db.Departments.Add(entity);
                db.SaveChanges();
            }
        }

        public PageGridData<DepartmentDto> GetDepartmentPage(DataAccess.Query.DepartmentQuery query)
        {
            using (var dbScope = _dbScopeFactory.CreateReadOnly())
            {
                var db = dbScope.DbContexts.Get<SEMSDbContext>();
                var data = db.Departments.Select(r => new DepartmentDto
                {
                    Name = r.DepartmentName,
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
