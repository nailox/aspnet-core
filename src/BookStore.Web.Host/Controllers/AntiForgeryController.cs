using BookStore.Controllers;
using Microsoft.AspNetCore.Antiforgery;

namespace BookStore.Web.Host.Controllers
{
    public class AntiForgeryController : BookStoreControllerBase
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