using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleAPI.Contracts.v1.Responses
{
    public class AuthReponse
    {
        public string Token { get; set; }
        public bool Success { get; set; }
        public ErrorResponse Error { get; set; }
    }
}
