using DataAccessLayer.Abstract;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Validation
{
    public class ExistingUserPhoneNumber
    {
        //public static async Task<bool> CheckPhoneNumberExists(string phoneNumber, CancellationToken cancellationToken)
        //{
        //    return await _clientRepository.ControlUserPhoneNumber(_httpContextAccessor.HttpContext.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier)?.Value, phoneNumber);
        //}
    }
}
