using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiTest.Model;

namespace WebApiTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        public LoginController(VerifyUser verify)
        {
            _verify = verify;
        }

        public VerifyUser _verify { get; }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult<string> Verify(LoginModel model)
        {
            if (_verify.Verify(model))
            {
                return _verify.GenerateToken(model.LoginAccount);
            }
            else
            {
                return BadRequest();
            }
           
        }
    }
}
