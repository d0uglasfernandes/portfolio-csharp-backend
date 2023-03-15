using Portfolio.Application.Controllers.BaseCrud;
using Portfolio.Domain.Command.Company;
using Portfolio.Domain.Query.Company;
using Microsoft.Extensions.Logging;
using MediatR;

namespace Portfolio.Application.Controllers.Company
{
    public class CompaniesController 
        : BaseCrudController<CompanyQuery, CompanyCreateCommand, CompanyUpdateCommand, CompanyDeleteCommand>
    {
        public CompaniesController(
            IMediator mediator,
            ILogger<CompaniesController> logger
        ) : base(mediator, logger) { }
    }
}