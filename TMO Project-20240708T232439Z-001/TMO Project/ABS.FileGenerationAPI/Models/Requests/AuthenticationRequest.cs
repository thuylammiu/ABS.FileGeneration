﻿namespace ABS.FileGenerationAPI.Models.Requests
{
    public class AuthenticationRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public string Role { get; set; }
    }
}
