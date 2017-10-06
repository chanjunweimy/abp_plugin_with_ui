using Todo.MainProject.Controllers;
using Microsoft.AspNetCore.Antiforgery;

namespace Todo.MainProject.Web.Host.Controllers
{
    public class AntiForgeryController : MainProjectControllerBase
    {
        private readonly IAntiforgery _antiforgery;

        public AntiForgeryController(IAntiforgery antiforgery)
        {
            _antiforgery = antiforgery;
        }

        public void GetToken()
        {
            _antiforgery.SetCookieTokenAndHeader(HttpContext);
        }
    }
}