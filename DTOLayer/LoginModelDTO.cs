﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOLayer
{
    public class LoginModelDTO
    {
        public string? Mail { get; set; }
        public string? Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
