using BusinessLayer.Abstract;
using FluentValidation;
using MatchEstate.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using BusinessLayer.Services;
using Shared.Wrappers;
using Shared.Dtos.PropertyListing;

namespace MatchEstate.Controllers
{
    public class PropertyListingController : BaseController
    {
        private readonly IPropertyListingService _listingService;
        private readonly IPropertyRequestService _requestService;
        private readonly IClientService _clientService;
        private readonly IPropertyService _propertyService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<AddListingDTO> _addListingDtoValidator;
        private readonly IValidator<UpdateListingDto> _updateListingDtoValidator;
        private readonly IValidator<FinalizeListingDto> _finalizeListingDtoValidator;
        public PropertyListingController(IPropertyListingService listingService,
            IClientService clientService,
            IPropertyService propertyService,
            IUnitOfWork unitOfWork,
            IValidator<AddListingDTO> listingModelValidator,
            IValidator<UpdateListingDto> updateListingDtoValidator,
            IPropertyRequestService requestService,
            IValidator<FinalizeListingDto> finalizeListingDtoValidator)
        {
            _listingService = listingService;
            _clientService = clientService;
            _propertyService = propertyService;
            _unitOfWork = unitOfWork;
            _addListingDtoValidator = listingModelValidator;
            _updateListingDtoValidator = updateListingDtoValidator;
            _requestService = requestService;
            _finalizeListingDtoValidator = finalizeListingDtoValidator;
        }

        public IActionResult Index()
        {
            ViewBag.title = "Property Listings Page";
            return View();
        }

        public IActionResult AddListing()
        {
            ViewBag.title = "Add listing Page";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteListing(string idsJson)
        {
            var listingIds = JsonConvert.DeserializeObject<List<string>>(idsJson);
            _listingService.DeleteRange(UserId, listingIds);
            var result = await _unitOfWork.SaveChanges();
            var response = new BaseResponse();
            
            if (result <= 0)
            {
                response.Success = false;
                response.Message = "An error occured while deleting listings.";
            }

            response.Success = true;
            response.Message = "Selected listings have been deleted successfully.";
            
            return Ok(response);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddListing(AddListingDTO listingModel)
        {
            var validateResult = await _addListingDtoValidator.ValidateAsync(listingModel);

            if (validateResult.IsValid)
            {
                var result = await _listingService.Insert(UserId, listingModel);
                if (!result)
                {
                    ViewBag.error = "An error occured while saving listing.";
                    return View();
                }
                TempData["success"] = "Listing has been saved successfully.";
                return RedirectToAction("Index");
            }
            else
            {
                IEnumerable<string> allErrors = validateResult.Errors.Select(x => x.ErrorMessage);
                ViewBag.error = ValidationMessageWriter.MessageWriter(allErrors);
                return View();
            }
        }

        [Route("propertylisting/UpdateListing/{listingId}")]
        [HttpGet]
        public IActionResult UpdateListing(string listingId)
        {
            UpdateListingDto? dto = _listingService.GetListingForUpdate(UserId, listingId);
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateListing(UpdateListingDto dto)
        {
            var validateResult = await _updateListingDtoValidator.ValidateAsync(dto);

            if (validateResult.IsValid)
            {
                var result = await _listingService.Update(UserId, dto);
                if (!result)
                {
                    ViewBag.error = "An error occured while updating listing.";
                    return View(dto);
                }
                TempData["success"] = "Listing updated successfully.";
                return RedirectToAction("Index");
            }

            IEnumerable<string> allErrors = validateResult.Errors.Select(x => x.ErrorMessage);
            ViewBag.error = ValidationMessageWriter.MessageWriter(allErrors);
            return View(dto);
        }

        [Route("/propertylisting/finalizelisting/{id}")]
        public async Task<IActionResult> FinalizeListing(string id, FinalizeListingDto dto)
        {
            var validateResult = await _finalizeListingDtoValidator.ValidateAsync(dto);
            var response = new BaseResponse();

            if (validateResult.IsValid)
            {
                bool listingResult = await _listingService.FinalizeListing(UserId, id, dto.Earning, dto.RequestId);
                bool requestResult = true;
                if (dto.RequestId != "0")
                    requestResult = await _requestService.FinalizeRequest(UserId, dto.RequestId);

                var saved = await _unitOfWork.SaveChanges();

                if (!listingResult || !requestResult || saved <= 0)
                {
                    response.Success = false;
                    response.Message = "An error occured while marking listing as finalized.";
                }
                else
                {
                    response.Success = true;
                    response.Message = "Listing has been marked as finalized successfully.";
                }

                return Ok(response);
            }

            response.Success = false;
            IEnumerable<string> allErrors = validateResult.Errors.Select(x => x.ErrorMessage);
            response.Message = ValidationMessageWriter.MessageWriter(allErrors);

            return Ok(response);
        }

        public async Task<IActionResult> Detail(string id)
        {
            ViewBag.title = "Listing Detail Page";
            var dto = await _listingService.GetListingDetail(UserId, id);
            return View(dto);
        }

        public IActionResult GetByFilters(ListingGetByFiltersDTO getByFiltersDTO)
        {
            var data = _listingService.GetByFilters(UserId, getByFiltersDTO);
            var response = new DataResponse<ListingPageResponseModel>()
            {
                Success = true,
                Message = "",
                Data = new ListingPageResponseModel
                {
                    Listings = data.Item1,
                    TotalListingCount = data.Item2
                }
            };
            return Ok(response);
        }
    }
}
