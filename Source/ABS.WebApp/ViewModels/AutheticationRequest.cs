using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ABS.WebApp.ViewModels
{
    public class AutheticationRequest
    {
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public string Role { get; set; }


    }
}