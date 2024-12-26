using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DTOLayer;
using EntityLayer.Entities;
using MatchEstate.Wrappers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MatchEstate.Controllers
{
    public class ClientController : BaseController
    {
        private readonly IClientService _clientService;
        private readonly IUnitOfWork _unitOfWork;
        public ClientController(IClientService clientService, IUnitOfWork unitOfWork)
        {
            _clientService = clientService;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            ViewBag.title = "Clients Page";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteClient(List<string> clientsIds)
        {
            _clientService.DeleteRange(UserId, clientsIds);
            var result = await _unitOfWork.SaveChanges();
            if (result <= 0)
            {
                return Ok(new BaseResponse { Success = false, Message = "An error occuered while deleting the client. Please try again later" });
            }
            return Ok(new BaseResponse { Success = true, Message = "Client has been deleted successfully" });
        }

        public IActionResult SearchClient(string search)
        {
            IEnumerable<ClientPageDTO> clients = _clientService.SearchClient(UserId, search);
            var response = new DataResponse<IEnumerable<ClientPageDTO>>()
            {
                Success = true,
                Message = "",
                Data = clients
            };

            return Ok(response);
        }
    }
}
