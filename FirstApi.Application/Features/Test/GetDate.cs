using Mediator.Mediator;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FirstApi.Application
{
    public class GetDate
    {
        public class Request : BaseRequest<Response>
        {
            public int Num { get; set; }
        }

        public class Response : BaseResponse
        {
            public Response()
            {
            }

            //public DateTime Date = DateTime.Now;
            public int redenBroj {get; set;}
        }
        public class Handler : BaseHandler<Request, Response>
        {


            public override async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                return new Response { redenBroj = request.Num };
            }


           
        }
    }
}
