using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Threading;

namespace Portfolio.Application.Controllers.BaseControllers.Interfaces
{
    public interface IBaseDeleteController<CommandEntity>
        where CommandEntity : class
    {
        Task<IActionResult> DeleteAsync([FromBody] CommandEntity value, CancellationToken cancellationToken);
    }
}
