using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;
using Shared.Wrappers;
using Shared.Dtos.Client;
using EntityLayer.Entities;

namespace MatchEstate.Controllers
{
    public class ClientController : BaseController
    {
        private readonly IClientService _clientService;
        private readonly IPropertyListingService _listingService;
        private readonly IPropertyRequestService _requestService;
        private readonly IUnitOfWork _unitOfWork;
        public ClientController(IClientService clientService, IUnitOfWork unitOfWork, IPropertyListingService listingService, IPropertyRequestService requestService)
        {
            _clientService = clientService;
            _unitOfWork = unitOfWork;
            _listingService = listingService;
            _requestService = requestService;
        }

        public IActionResult Index()
        {
            ViewBag.title = "Clients Page";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteClient(List<string> clientsIds)
        {
            var listings = new List<PropertyListing>();
            var requests = new List<PropertyRequest>();
            foreach (var clientId in clientsIds)
            {
                var listingsToAdd = await _listingService.GetListingsByClient(UserId, clientId);
                listings.AddRange(listingsToAdd);

                var requestsToAdd = await _requestService.GetRequestsByClient(UserId, clientId);
                requests.AddRange(requestsToAdd);
            }

            _listingService.DeleteRange(listings);
            _requestService.DeleteRange(requests);

            _clientService.DeleteRange(UserId, clientsIds);

            var result = await _unitOfWork.SaveChanges();
            if (result <= 0)
                return Ok(new BaseResponse { Success = false, Message = "An error occuered while deleting the client. Please try again later" });

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
