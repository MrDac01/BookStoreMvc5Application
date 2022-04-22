using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStoreMvc5Application
{
    public class MappingConfig
    {
        public static void Register()
        {
            AutoMapper.Mapper.Initialize(config =>
            {
                config.AddProfiles(typeof(MappingConfig).Assembly);
            });

            AutoMapper.Mapper.AssertConfigurationIsValid();
        }
    }
}