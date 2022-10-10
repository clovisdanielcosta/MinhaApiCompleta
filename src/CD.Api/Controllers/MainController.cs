using Microsoft.AspNetCore.Mvc;

namespace CD.Api.Controllers
{
    [ApiController]
    public abstract class MainController : ControllerBase
    {
       
    }

    [Route("api/[controller]")]
    public class FornecedoresController : MainController
    {

    }

}