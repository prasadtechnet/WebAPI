using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleAPI.Contracts.v1
{
    public static class APIRoutes
    {
        private const string Root = "api";
        private const string Version = "v1";

        private const string Base = Root + "/"+Version;

        public static class Order
        {
            public const string GetAll = Base+"/GetAll";

            public const string Get = Base + "/GetById/{Id}";

        }

        public static class Auth
        {
            public const string Login = Base + "/login";

            public const string Register = Base + "/register";

        }
    }
}
