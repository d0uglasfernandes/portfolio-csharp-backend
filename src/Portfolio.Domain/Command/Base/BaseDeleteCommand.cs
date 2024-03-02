using System;

namespace Portfolio.Domain.Command.Base
{
    public class BaseDeleteCommand : BaseCommand
    {
        public Guid Id { get; set; }
    }
}