﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleAPI.Contracts.v1.Responses
{
    public class ErrorResponse
    {
        public List<string> Errors { get; set; }
    }
}
