using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using BusinessLayer.Mapping;
using BusinessLayer.Validation;
using DTOLayer;
using EntityLayer.Entities;
using FluentValidation;
using MatchEstate.Models;
using MatchEstate.Wrappers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MatchEstate.Controllers
{
    public class RequestController : BaseController
    {
        private readonly IPropertyRequestService _requestService;
        private readonly IPropertyListingService _listingService;
        private readonly IClientService _clientService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<RequestModelDTO> _validator;

        public RequestController(IPropertyRequestService requestService, IPropertyListingService listingService, IClientService clientService, IUnitOfWork unitOfWork, IValidator<RequestModelDTO> validator)
        {
            _requestService = requestService;
            _listingService = listingService;
            _clientService = clientService;
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public IActionResult Index()
        {
            ViewBag.title = "Property Requests Page";
            return View();
        }

        public IActionResult AddRequest()
        {
            ViewBag.title = "Add Request Page";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRequest(string idsJson)
        {
            var requestIds = JsonConvert.DeserializeObject<List<string>>(idsJson);
            _requestService.DeleteRange(UserId, requestIds);
            var result = await _unitOfWork.SaveChanges();
            var response = new BaseResponse();
            if (result <= 0)
            {
                response.Success = false;
                response.Message = "An error occured while deleting request. Please try again later";
            }
            else
            {
                response.Success = true;
                response.Message = "Selected requests have been deleted.";
            }

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> AddRequest(RequestModelDTO requestModel)
        {
            var validateResult = await _validator.ValidateAsync(requestModel);
            var response = new BaseResponse();
            if (validateResult.IsValid)
            {
                var result = await _requestService.Insert(UserId, requestModel);
                if (result.Item1)
                {
                    TempData["success"] = result.Item2;
                    response.Success = true;
                }
                else
                {
                    response.Success = false;
                    response.Message = result.Item2;
                }
            }
            else
            {
                IEnumerable<string> allErrors = validateResult.Errors.Select(x => x.ErrorMessage);
                response.Success = false;
                response.Message = ValidationMessageWriter.MessageWriter(allErrors);
            }

            return Ok(response);
        }

        [Route("Request/UpdateRequest/{requestId}")]
        [HttpGet]
        public async Task<IActionResult> UpdateRequest(string requestId)
        {
            PropertyRequest request = await _requestService.GetWithClient(UserId, requestId);
            RequestModelDTO dto = RequestMapper.MapToRequestModelDTO(request);
            dto.RadioForClient = "0";
            ViewBag.requestId = requestId;
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateRequest(string requestId, RequestModelDTO requestModel)
        {
            var validateResult = await _validator.ValidateAsync(requestModel);
            var response = new BaseResponse();
            if (validateResult.IsValid)
            {
                var result = await _requestService.Update(UserId, requestId, requestModel);
                if (result.Item1)
                {
                    TempData["success"] = result.Item2;
                    response.Success = true;
                }
                else
                {
                    response.Success = false;
                    response.Message = result.Item2;
                }
            }
            else
            {
                IEnumerable<string> allErrors = validateResult.Errors.Select(x => x.ErrorMessage);
                response.Success = false;
                response.Message = ValidationMessageWriter.MessageWriter(allErrors);
            }

            return Ok(response);
        }

        public async Task<IActionResult> Detail(string id)
        {
            ViewBag.title = "Request Detail Page";

            var request = await _requestService.GetWithClient(UserId, id);
            var districts = JsonConvert.DeserializeObject<List<string>>(request.District);
            var rooms = JsonConvert.DeserializeObject<List<string>>(request.NumberOfRooms);
            ViewBag.DistrictsAndRooms = new {
                districts = districts,
                rooms = rooms
            };
            ViewBag.UserId = UserId;
            return View(request);
        }

        public IActionResult GetByFilters(RequestGetByFiltersDTO getByFilters)
        {
            var data = _requestService.GetByFilters(UserId, getByFilters);
            var response = new DataResponse<RequestPageResponseModel>()
            {
                Success = true,
                Message = "",
                Data = new RequestPageResponseModel
                {
                    Requests = data.Item1,
                    TotalRequestCount = data.Item2
                }
            };
            return Ok(response);
        }
    }
}
