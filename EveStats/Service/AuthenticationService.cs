using EveStats.Data.Resources;
using System;
using System.Net;
using System.Text;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.IdentityModel.Tokens.Jwt;

namespace EveStats.Service
{
    public sealed class AuthenticationService
    {
        public static AuthenticationService GetInstance()
        {
            AuthenticationService authService;

            return authService;
        }
    }
}
