using ABS.FileGenerationAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABS.FileGenerationAPI.Services.Tokens
{
    public interface ITokenService
    {
        public string GenerateToken(AppUser user);
    }
}
