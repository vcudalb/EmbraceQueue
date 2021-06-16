using Microsoft.AspNetCore.Mvc;

namespace EmbraceQueue.API.V1.Controllers
{
    /// <summary>
    /// Base controller, used for setting common attributes
    /// </summary>
    [ApiVersion("1")]
    [ApiController]
    public class ApiController : ControllerBase
    {
    }
}
