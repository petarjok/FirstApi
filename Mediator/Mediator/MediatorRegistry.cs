using MediatR;
using Microsoft.AspNetCore.Http;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mediator.Mediator
{
    public class MediatorPipelineRegistry : Registry
    {
        public MediatorPipelineRegistry(string projectApplicationAssembly)
        {
            Scan(scanner =>
            {
                scanner.TheCallingAssembly();
                scanner.Assembly(projectApplicationAssembly);
                scanner.AssemblyContainingType(typeof(IRequestHandler<,>));

                //scanner.ConnectImplementationsToTypesClosing(typeof(FluentValidation.IValidator<>));
                //scanner.ConnectImplementationsToTypesClosing(typeof(BaseValidator<,>));
                scanner.ConnectImplementationsToTypesClosing(typeof(IRequestHandler<,>));
                //scanner.ConnectImplementationsToTypesClosing(typeof(INotificationHandler<>));
            });

            For<ServiceFactory>().Use<ServiceFactory>(ctx => t => ctx.GetInstance(t));
            For<IMediator>().Use<MediatR.Mediator>();
            //For<DbContext>().Use<HalzaDbContext>();
            //For<IHttpContextAccessor>().Singleton().Use<HttpContextAccessor>();

            // Configure decorators over feature handlers
            For(typeof(IRequestHandler<,>)).DecorateAllWith(typeof(MediatorPipelineHandler<,>));
        }
    }
}
