using System;

namespace Portfolio.Domain.Command.Base
{
    public class BaseUpdateCommand : BaseCommand
    {
        public Guid Id { get; set; }
    }
}