using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RoofStock.Data.AutoMapper
{
    public static class AutoMapperProfiles
    {
        public static IEnumerable<Type> GetAutoMapperProfiles()
        {
            return Assembly.GetExecutingAssembly()
                  .GetTypes()
                  .Where(t => t.IsSubclassOf(typeof(Profile)));
        }
    }
}
