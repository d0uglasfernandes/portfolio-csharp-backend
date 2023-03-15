using MediatR;

namespace Portfolio.Domain.Command.Base
{
    public class BaseCommand : IRequest<CommandBaseResult>
    {
    }
}