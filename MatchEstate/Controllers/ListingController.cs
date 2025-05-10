using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using BusinessLayer.Mapping;
using DTOLayer;
using EntityLayer.Entities;
using FluentValidation;
using MatchEstate.Models;
using MatchEstate.Wrappers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MatchEstate.Controllers
{
    public class ListingController : BaseController
    {
        private readonly IPropertyListingService _listingService;
        private readonly IClientService _clientService;
        private readonly IPropertyService _propertyService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<AddListingDTO> _listingModelValidator;
        public ListingController(IPropertyListingService listingService, IClientService clientService, IPropertyService propertyService, IUnitOfWork unitOfWork, IValidator<AddListingDTO> listingModelValidator)
        {
            _listingService = listingService;
            _clientService = clientService;
            _propertyService = propertyService;
            _unitOfWork = unitOfWork;
            _listingModelValidator = listingModelValidator;
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
            _listingService.DeleteRange(listingIds);
            var result = await _unitOfWork.SaveChanges();
            var response = new BaseResponse();
            
            if (result <= 0)
            {
                response.Success = false;
                response.Message = "An error occured while deleting listings";
            }

            response.Success = true;
            response.Message = "Selected listings have been deleted successfully";
            
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> AddListing(AddListingDTO listingModel)
        {
            var validateResult = await _listingModelValidator.ValidateAsync(listingModel);

            if (validateResult.IsValid)
            {
                var result = await _listingService.Insert(UserId, listingModel);
                if (!result.Item1)
                {
                    ViewBag.error = result.Item2;
                    return View();
                }
                TempData["success"] = result.Item2;
                return RedirectToAction("Index");
            }
            else
            {
                IEnumerable<string> allErrors = validateResult.Errors.Select(x => x.ErrorMessage);
                ViewBag.error = ValidationMessageWriter.MessageWriter(allErrors);
                return View();
            }
        }

        [Route("Listing/UpdateListing/{listingId}")]
        [HttpGet]
        public async Task<IActionResult> UpdateListing(string listingId)
        {
            PropertyListing listing = await _listingService.GetWithClient(UserId, listingId);
            AddListingDTO dto = ListingMapper.MapToAddListingDTO(listing);
            dto.RadioForClient = "0";
            ViewBag.listingId = listingId;
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateListing(string listingId, AddListingDTO listingModel)
        {
            var validateResult = await _listingModelValidator.ValidateAsync(listingModel);

            if (validateResult.IsValid)
            {
                var result = await _listingService.Update(UserId, listingId, listingModel);
                if (!result.Item1)
                {
                    ViewBag.error = result.Item2;
                    return View();
                }
                TempData["success"] = result.Item2;
                return RedirectToAction("Index");
            }
            
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> SellListing(string id, [FromQuery] string earning)
        {
            bool result = await _listingService.SellListing(id, earning);
            var saved = await _unitOfWork.SaveChanges();
            var response = new BaseResponse();
            
            if (!result || saved<=0)
            {
                response.Success = false;
                response.Message = "An error occured while marking listing as sold/rented.";
            }
            else
            {
                response.Success = true;
                response.Message = "Listing has been marked as sold/rented.";
            }

            return Ok(response);
        }

        public async Task<IActionResult> Detail(string id)
        {
            ViewBag.title = "Listing Detail Page";
            var listing = await _listingService.GetWithClient(UserId, id);
            return View(listing);
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
