using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;

namespace Authe
{
    public class AuthorizedAttribute:System.Web.Http.AuthorizeAttribute
    {
      protected override void  HandleUnauthorizedRequest(HttpActionContext context)
        {
            if(HttpContext.Current.User.Identity.IsAuthenticated)
            {
                base.HandleUnauthorizedRequest(context);

            }
            else
            {
                context.Response = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Forbidden);
            }
        }
    }
}