using EmitMapper;
using EmitMapper.MappingConfiguration;
using SEMS.DataAccess.Dto;
using SEMS.DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEMS.DataAccess.EmitMapper
{
    public class EmitMapper
    {
        public static void EmitMapperRegister()
        {
            ObjectMapperManager.DefaultInstance.GetMapper<Company, CompanyDto>(new DefaultMapConfig().MatchMembers((x, y) =>
            {
                if (x == "address" && y == "userAddress")
                {
                    return true;
                }
                return x == y;
            }));

           
        }
    }
}
