using Portfolio.Application.Controllers.BaseCrud;
using Portfolio.Domain.Command.Company;
using Portfolio.Domain.Query.Company;
using MediatR;

namespace Portfolio.Application.Controllers.Company
{
    public class CompaniesController(
        IMediator mediator,
        ILogger<CompaniesController> logger)
        : BaseCrudController<CompanyQuery, CompanyCreateCommand, CompanyUpdateCommand, CompanyDeleteCommand>(mediator, logger)
    {
    }
}