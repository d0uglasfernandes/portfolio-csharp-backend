using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Threading;

namespace Portfolio.Application.Controllers.BaseControllers.Interfaces
{
    public interface IBasePutController<CommandEntity>
        where CommandEntity : class
    {
        Task<IActionResult> PutAsync([FromBody] CommandEntity value, CancellationToken cancellationToken);
    }
}
