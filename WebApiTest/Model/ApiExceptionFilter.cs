using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Net;

namespace WebApiTest.Model
{
    public class ApiExceptionFilter:ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            base.OnException(context);

            context.ExceptionHandled = true;

            var response=context.HttpContext.Response;
            response.StatusCode = (int)HttpStatusCode.OK;
            response.ContentType = "application/json";
            context.Result = new ObjectResult(new {TimeStamp=DateTime.Now, Message="System Error", ErrorCode=500});
        }
    }
}
