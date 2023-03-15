using AutoMapper;
using Portfolio.CrossCutting.Mapping.Company;

namespace Portfolio.CrossCutting.Mappings
{
    public static class MapperProfile 
    {
        public static MapperConfiguration Configure()
        {
            var config = new AutoMapper.MapperConfiguration(cfg => {
                cfg.AddProfile(new CompanyProfile());
            });

            return config;
        }
    }
}