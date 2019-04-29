using System.Net;
using System.Web.Http;
using System.Web.Http.Results;

namespace GestaoContratos.Controllers
{
    public class BaseApiController : ApiController
    {
        public StatusCodeResult NoContent()
        {
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}