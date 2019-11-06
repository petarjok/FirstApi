using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FirstApi.Application;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FirstApi.Controllers.Test
{
    public class TestController : BaseController
    {
        public TestController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet("getDate")]
        public async Task<GetDate.Response> GetDate(GetDate.Request request)
        {
            var response = await Handle(request);
            return response;
        }
    }
    

}