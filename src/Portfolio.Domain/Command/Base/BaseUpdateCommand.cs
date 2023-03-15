using System;
using MediatR;

namespace Portfolio.Domain.Command.Base
{
    public class BaseUpdateCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}