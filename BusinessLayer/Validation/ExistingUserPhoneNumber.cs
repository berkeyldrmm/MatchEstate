using EntityLayer.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Validation
{
    public static class ExistingUserPhoneNumber
    {
        public static bool ControlUserPhoneNumber(UserManager<User> userManager, string phoneNumber)
        {
            User user = userManager.Users.FirstOrDefault(u => u.PhoneNumber == phoneNumber);
            return user == null;
        }
    }
}
