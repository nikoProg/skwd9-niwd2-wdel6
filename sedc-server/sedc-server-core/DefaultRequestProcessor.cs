﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sedc.Server.Core
{
    class DefaultRequestProcessor : IRequestProcessor
    {
        public IResponse Process(Request request)
        {
            return new TextResponse
            {
                Message = $"Hi, I'm a SEDC Server, nice to meet you! You used the {request.Method} method on the {request.Address} URL",
                Status = Status.OK
            };
        }
    }
}
