using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace Authe.Controllers
{
    public class ValueController : ApiController
    {
        [AllowAnonymous]
        [HttpGet]
        [Route("api/Value/forall")]
        public IHttpActionResult Get()
        {
            return Ok("today date is" + DateTime.Now);
        }
        [Authorize]
        [HttpGet]
        [Route("api/Value/Authorize")]
        public IHttpActionResult GetAuthorize()
        {
            var identity = (ClaimsIdentity)User.Identity;
            return Ok("hell" + identity.Name);
        }
        [Authorize(Roles ="admin")]
        [HttpGet]
        [Route("api/Value/adminAuthorize")]
        public IHttpActionResult GetAdmin()
        {
            var identity = (ClaimsIdentity)User.Identity;
            var roles = identity.Claims.Where(x => x.Type == ClaimTypes.Role).Select(x=>x.Value);
            return Ok(roles);
        }
    }
}
