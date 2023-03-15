using System;
using MediatR;

namespace Portfolio.Domain.Command.Base
{
    public class BaseDeleteCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}