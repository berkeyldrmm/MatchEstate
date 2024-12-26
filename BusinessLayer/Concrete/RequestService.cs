using Azure;
using BusinessLayer.Abstract;
using DataAccessLayer;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DTOLayer;
using EntityLayer.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class RequestService : IRequestService
    {
        private readonly IRequestDal _reqeustRepository;
        private readonly IClientService _clientService;
        private readonly IPropertyService _propertyService;
        private readonly IUnitOfWork _unitOfWork;
        public RequestService(IRequestDal reqeustRepository, IClientService clientService, IPropertyService propertyService, IUnitOfWork unitOfWork)
        {
            _reqeustRepository = reqeustRepository;
            _clientService = clientService;
            _propertyService = propertyService;
            _unitOfWork = unitOfWork;
        }

        public bool DeleteAsync(PropertyRequest item)
        {
            return _reqeustRepository.Delete(item);
        }

        public void DeleteRange(string userId, IEnumerable<string> Ids)
        {
            var items = _reqeustRepository.GetRange(userId, Ids);
            _reqeustRepository.DeleteRange(items);
        }

        public Task<IEnumerable<PropertyRequest>> GetAll()
        {
            return _reqeustRepository.ReadAll();
        }

        public async Task<IEnumerable<PropertyRequest>> GetAllWithClient(string userId)
        {
            return await _reqeustRepository.GetAllWithClient(userId);
        }

        public object GetCountsOfRequestTypes(string userId)
        {
            return _reqeustRepository.GetCountsOfRequestTypes(userId);
        }

        public Task<PropertyType> GetPropertyType(int id)
        {
            return _reqeustRepository.GetPropertyType(id);
        }

        public Task<PropertyRequest> GetOne(string id)
        {
            return _reqeustRepository.Read(id);
        }

        public object GetForSaleOrRent(string userId)
        {
            return _reqeustRepository.GetForSaleOrRent(userId);
        }

        public async Task<PropertyRequest> GetWithClient(string userId, string id)
        {
            return await _reqeustRepository.GetWithClient(userId, id);
        }

        public async Task<(bool, string)> Insert(string userId, RequestModelDTO requestModel)
        {
            PropertyRequest request = new PropertyRequest()
            {
                Id = Guid.NewGuid().ToString(),
                District = JsonConvert.SerializeObject(requestModel.District),
                NumberOfRooms = JsonConvert.SerializeObject(requestModel.NumberOfRooms),
                IsForSaleOrRent = requestModel.IsForSaleOrRent,
                UserId = userId,
                MinimumPrice = requestModel.MinPrice ?? 0,
                MaximumPrice = requestModel.MaxPrice ?? 0,
                Title = requestModel.RequestTitle,
                City = requestModel.City
            };
            request.PropertyTypeId = Convert.ToInt32(requestModel.PropertyType);
            request.PropertyType = await this.GetPropertyType(request.PropertyTypeId);
            if (requestModel.RadioForClient == "1")
            {
                bool phoneNumberCheck = _clientService.ControlUserPhoneNumber(userId, requestModel.ClientPhoneNumber);
                if (!phoneNumberCheck)
                {
                    return (false, "A client with the same phone number already exists.");
                }

                var client = new Client()
                {
                    Id = Guid.NewGuid().ToString(),
                    NameSurname = requestModel.ClientNameSurname,
                    Email = requestModel.ClientEmail,
                    PhoneNumber = requestModel.ClientPhoneNumber,
                    UserId = userId
                };

                request.ClientId = client.Id;
                request.Client = client;
            }
            else
            {
                request.ClientId = requestModel.ClientId;
                request.Client = await _clientService.GetOne(requestModel.ClientId);
            }

            request.AddedDate = DateTime.Now;

            if (requestModel.IsForSaleOrRent == "0")
                request.IsForSaleOrRent = "For Rent";
            else
                request.IsForSaleOrRent = "For Sale";

            request.Details = requestModel.Details ?? "";
            var result = await _reqeustRepository.Insert(request);
            int saved = await _unitOfWork.SaveChanges();
            if (result && saved > 0)
            {
                //TempData["success"] = "Request has been saved successfully.";
                //response.Success = true;
                //response.Message = "";
                //return Ok(response);
                return (true, "Request has been saved successfully.");
            }
            //response.Success = false;
            //response.Message = "An error occured while adding the request.";
            //return Ok(response);
            return (false, "An error occured while adding the request.");
        }

        public Task<bool> Update(PropertyRequest item)
        {
            return _reqeustRepository.Update(item);
        }

        public Task<IEnumerable<PropertyRequestDTO>> GetByFilters(string userId, RequestGetByFiltersDTO getByFilers)
        {
            var expressions = new List<Expression<Func<PropertyRequest, bool>>>();

            if (getByFilers.IsForSaleOrRent != "0")
                expressions.Add(t => t.IsForSaleOrRent == getByFilers.IsForSaleOrRent);

            if (getByFilers.PropertyType != 0)
                expressions.Add(t => t.PropertyTypeId == getByFilers.PropertyType);

            if (getByFilers.Search != null && getByFilers.Search.Length > 2)
            {
                expressions.Add(t => t.Title.Contains(getByFilers.Search) || t.City.Contains(getByFilers.Search) || t.Client.NameSurname.Contains(getByFilers.Search));
            }

            return _reqeustRepository.GetByFilters(userId, expressions, getByFilers.Sort);
        }

        public async Task<List<PropertyRequest>> GetRequestsForListing(string userId, PropertyListing ilan)
        {
            List<Expression<Func<PropertyRequest, bool>>> expressions = new List<Expression<Func<PropertyRequest, bool>>>();
            expressions.Add(t => t.District.Contains("\""+ilan.District+"\"") && t.City == ilan.City);
            expressions.Add(t => t.MinimumPrice <= ilan.Price && t.MaximumPrice >= ilan.Price);

            if (ilan.PropertyTypeId == 5)
            {
                expressions.Add(t => t.NumberOfRooms.Contains("\""+ilan.Apartment.NumberOfRooms+"\""));
            }
            else if (ilan.PropertyTypeId == 1)
            {
                expressions.Add(t => t.NumberOfRooms.Contains("\"" + ilan.Shop.NumberOfRooms + "\""));
            }

            expressions.Add(t => t.IsForSaleOrRent == ilan.IsForSaleOrRent);

            expressions.Add(t => t.PropertyTypeId == ilan.PropertyTypeId);

            expressions.Add(t => t.Client.Id != ilan.Client.Id);

            return await _reqeustRepository.GetRequestsForListing(userId, expressions);
        }

        public async Task<bool> Insert(PropertyRequest item)
        {
            return await _reqeustRepository.Insert(item);
        }
    }
}
