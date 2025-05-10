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
    public class PropertyRequestService : IPropertyRequestService
    {
        private readonly IPropertyRequestRepository _reqeustRepository;
        private readonly IClientService _clientService;
        private readonly IPropertyService _propertyService;
        private readonly IUnitOfWork _unitOfWork;
        public PropertyRequestService(IPropertyRequestRepository reqeustRepository, IClientService clientService, IUnitOfWork unitOfWork, IPropertyService propertyService)
        {
            _reqeustRepository = reqeustRepository;
            _clientService = clientService;
            _propertyService = propertyService;
            _unitOfWork = unitOfWork;
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
                //PropertyStatusId = requestModel.PropertyStatusId,
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

            if (requestModel.PropertyStatusId == "0")
                request.PropertyStatusId = 0;
            else
                request.PropertyStatusId = 1;

            request.Details = requestModel.Details ?? "";
            var result = await _reqeustRepository.Insert(request);
            int saved = await _unitOfWork.SaveChanges();
            if (result && saved > 0)
            {
                return (true, "Request has been saved successfully.");
            }
            return (false, "An error occured while adding the request.");
        }

        public (IEnumerable<RequestPageDTO>, int) GetByFilters(string userId, RequestGetByFiltersDTO getByFilers)
        {
            var expressions = new List<Expression<Func<PropertyRequest, bool>>>();

            //if (getByFilers.PropertyStatusId != "0")
            //    expressions.Add(t => t.PropertyStatusId == getByFilers.PropertyStatusId);

            if (getByFilers.PropertyType != 0)
                expressions.Add(t => t.PropertyTypeId == getByFilers.PropertyType);

            if (getByFilers.Search != null && getByFilers.Search.Length > 2)
            {
                expressions.Add(t => t.Title.Contains(getByFilers.Search) || t.City.Contains(getByFilers.Search) || t.Client.NameSurname.Contains(getByFilers.Search));
            }

            return _reqeustRepository.GetByFilters(userId, expressions, getByFilers.Sort, Convert.ToInt16(getByFilers.PageNumber), Convert.ToInt16(getByFilers.PageSize));
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

            expressions.Add(t => t.PropertyStatusId == ilan.PropertyStatusId);

            expressions.Add(t => t.PropertyTypeId == ilan.PropertyTypeId);

            expressions.Add(t => t.Client.Id != ilan.Client.Id);

            return await _reqeustRepository.GetRequestsForListing(userId, expressions);
        }

        public async Task<(bool, string)> Update(string userId, string requestId, RequestModelDTO requestModel)
        {
            var request = await _reqeustRepository.GetWithClient(userId, requestId);
            if (request == null)
            {
                return (false, "Request not found.");
            }

            request.Title = requestModel.RequestTitle;
            request.MaximumPrice = requestModel.MaxPrice ?? 0;
            //request.PropertyStatusId = requestModel.PropertyStatusId == "1" ? "For Sale" : "For Rent";
            request.City = requestModel.City;
            request.District = JsonConvert.SerializeObject(requestModel.District);
            request.NumberOfRooms = JsonConvert.SerializeObject(requestModel.NumberOfRooms);
            request.Details = requestModel.Details;

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
                    Id = request.ClientId ?? Guid.NewGuid().ToString(),
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

            var result = await _reqeustRepository.Update(request);
            int saved = await _unitOfWork.SaveChanges();

            if (result && saved > 0)
            {
                return (true, "Request has been updated successfully.");
            }

            return (false, "An error occurred while updating the request. Please try again later.");
        }
    }
}
