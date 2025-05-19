using BusinessLayer.Abstract;
using FluentValidation;
using MatchEstate.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shared.Wrappers;
using BusinessLayer.Services;
using Shared.Dtos.PropertyRequest;

namespace MatchEstate.Controllers
{
    public class PropertyRequestController : BaseController
    {
        private readonly IPropertyRequestService _requestService;
        private readonly IPropertyListingService _listingService;
        private readonly IClientService _clientService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<AddRequestDto> _addRequestDtovalidator;
        private readonly IValidator<UpdateRequestDto> _updateRequestDtoValidator;

        public PropertyRequestController(
            IPropertyRequestService requestService,
            IPropertyListingService listingService,
            IClientService clientService,
            IUnitOfWork unitOfWork,
            IValidator<AddRequestDto> addRequestDtovalidator,
            IValidator<UpdateRequestDto> updateRequestDtoValidator)
        {
            _requestService = requestService;
            _listingService = listingService;
            _clientService = clientService;
            _unitOfWork = unitOfWork;
            _addRequestDtovalidator = addRequestDtovalidator;
            _updateRequestDtoValidator = updateRequestDtoValidator;
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
        public async Task<IActionResult> AddRequest(AddRequestDto requestModel)
        {
            var validateResult = await _addRequestDtovalidator.ValidateAsync(requestModel);
            var response = new BaseResponse();
            if (validateResult.IsValid)
            {
                var result = await _requestService.Insert(UserId, requestModel);
                if (result)
                {
                    TempData["success"] = "Request has been saved successfully.";
                    response.Success = true;
                }
                else
                {
                    response.Success = false;
                    response.Message = "An error occured while saving request.";
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

        //[Route("PropertyRequest/UpdateRequest/{requestId}")]
        [HttpGet("[controller]/[action]/{requestId}")]
        public IActionResult UpdateRequest(string requestId)
        {
            UpdateRequestDto dto = _requestService.GetRequestForUpdate(UserId, requestId);
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateRequest(UpdateRequestDto requestModel)
        {
            var validateResult = await _updateRequestDtoValidator.ValidateAsync(requestModel);
            var response = new BaseResponse();
            if (validateResult.IsValid)
            {
                var result = await _requestService.Update(UserId, requestModel);
                if (result)
                {
                    TempData["success"] = "The request has been updated successfully.";
                    response.Success = true;
                }
                else
                {
                    response.Success = false;
                    response.Message = "An error occured while updating the request.";
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
