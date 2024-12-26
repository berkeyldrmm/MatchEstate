using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace RealEstate.ViewComponents
{
    public class GetClientsComponent : ViewComponent
    {
        private readonly IClientService _clientService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public GetClientsComponent(IClientService clientService, IHttpContextAccessor httpContextAccessor)
        {
            _clientService = clientService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var saticilar = await _clientService.GetClientsOfUser(_httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);

            return View(saticilar);
        }
    }
}
