using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ABS.WebApp.ViewModels
{
    public class AuthenticationResponse
    {
        public string UserName { get; set; }
        public string Token { get; set; }
    }
}