using Portfolio.Domain.Entities.Company;
using Portfolio.Domain.Dto.Company;
using Portfolio.Domain.Command.Company;
using Portfolio.Domain.Query.Company;
using AutoMapper;

namespace Portfolio.CrossCutting.Mapping.Company
{
    public class CompanyProfile : Profile
    {
        public CompanyProfile()
        {
            #region EntityToDto
            CreateMap<CompanyEntity, CompanyDto>().ReverseMap();
            CreateMap<CompanyEntity, CompanyCreateCommand>().ReverseMap();
            CreateMap<CompanyEntity, CompanyDeleteCommand>().ReverseMap();
            CreateMap<CompanyEntity, CompanyUpdateCommand>().ReverseMap();
            CreateMap<CompanyEntity, CompanyQuery>().ReverseMap();
            #endregion
        }
    }
}