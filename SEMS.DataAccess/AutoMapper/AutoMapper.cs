using AutoMapper;
using SEMS.DataAccess.Dto;
using SEMS.DataAccess.Dto.Base;
using SEMS.DataAccess.Model.Organization;
using SEMS.DataAccess.Organization.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEMS.DataAccess.AutoMapper
{
    public class AutoMapper
    {
        public static void MapperRegister()
        {
            // 字段名称不一样的映射写法
            // var exp = Mapper.CreateMap<BaseDto, Base>();
            // exp.ForMember(dto => dto.BasId, (map) => map.MapFrom(r => r.BasId));

            Mapper.CreateMap<CompanyDto, Company>();
            Mapper.CreateMap<DepartmentDto, Department>();
        }
    }
}
