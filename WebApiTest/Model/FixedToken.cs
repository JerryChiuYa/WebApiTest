using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Net;

namespace WebApiTest.Model
{
    public class FixedToken : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
           var request=context.HttpContext.Request;
            var token = request.Headers["TokenKey"];
            if (token.ToString() != "1234567")
            {
                var response = context.HttpContext.Response;
                response.StatusCode = (int)HttpStatusCode.Forbidden;
                response.ContentType = "application/json";
                context.Result = new ObjectResult(new {TimeStamp=DateTime.Now, Message="Token錯誤!", ErrorCode=403});
            }
        }
    }
}
