using MediatR;
using Portfolio.Domain.Dto.Result;

namespace Portfolio.Domain.Command.Base
{
    public class BaseCommand : IRequest<ResultDto>
    {
    }
}