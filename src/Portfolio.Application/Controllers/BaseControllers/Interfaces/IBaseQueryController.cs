using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Threading;

namespace Portfolio.Application.Controllers.BaseControllers.Interfaces
{
    public interface IBaseQueryController<QueryEntity>
        where QueryEntity : class
    {
        Task<IActionResult> GetAsync([FromQuery] QueryEntity query, CancellationToken cancellationToken);
    }
}
