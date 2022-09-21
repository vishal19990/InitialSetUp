using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Skillhouse.HRportal.Controllers
{
    [Authorize]
    [ApiController]
    public abstract class BaseController : Controller
    {
    }
}
