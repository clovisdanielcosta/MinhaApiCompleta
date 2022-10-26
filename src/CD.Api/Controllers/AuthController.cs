using CD.Business.Interfaces;

namespace CD.Api.Controllers
{
    public class AuthController : MainController
    {
        public AuthController(INotificador notificador) : base(notificador)
        {
        }
    }
}
