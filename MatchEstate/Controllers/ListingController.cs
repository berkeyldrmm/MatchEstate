using AutoMapper;
using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using BusinessLayer.Validation;
using DTOLayer;
using EntityLayer.Entities;
using FluentValidation;
using MatchEstate.Controllers;
using MatchEstate.Wrappers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace MatchEstate.Controllers
{
    public class ListingController : BaseController
    {
        private readonly IListingService _listingService;
        private readonly IClientService _clientService;
        private readonly IPropertyService _propertyService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<AddListingDTO> _listingModelValidator;
        public ListingController(IListingService listingService, IClientService clientService, IPropertyService propertyService, IUnitOfWork unitOfWork, IValidator<AddListingDTO> listingModelValidator)
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
            //if (listingModel.RadioForCommission == "0" && listingModel.Commission == null)
            //{
            //    ViewBag.error = "Please enter commission amount.";
            //    return View();
            //}
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
            IEnumerable<ListingPageDTO> listings = _listingService.GetByFilters(UserId, getByFiltersDTO);
            var response = new DataResponse<IEnumerable<ListingPageDTO>>()
            {
                Success = true,
                Message = "",
                Data = listings
            };
            return Ok(response);
        }
    }
}
