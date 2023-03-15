using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Threading;

namespace Portfolio.Application.Controllers.BaseControllers.Interfaces
{
    public interface IBasePostController<CommandEntity>
        where CommandEntity : class
    {
        Task<IActionResult> PostAsync([FromBody] CommandEntity value, CancellationToken cancellationToken);
    }
}
