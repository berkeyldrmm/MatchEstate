using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOLayer.Dtos
{
    public class LoginModelDTO
    {
        public string? UsernameOrMail { get; set; }
        public string? Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
