using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SCRDesktopApp
{
    internal class LoginResponse
    {
        public LoginResponse() { }
        [JsonPropertyName("email")]
        public string Email { get; set; }
        [JsonPropertyName("token")]
        public string Token { get; set; }

        public LoginResponse(string email, string token)
        {
            Email = email;
            Token = token;
        }
    }
}
