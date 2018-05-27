using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.OAuth;
using System.Web.Http;
using System.Net.Http;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

[assembly: OwinStartup(typeof(Authe.Startup))]

namespace Authe
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            var myprovider = new MyAuth();
            OAuthAuthorizationServerOptions options = new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan =  TimeSpan.FromDays(1),
                 Provider=myprovider
                
            };
            app.UseOAuthAuthorizationServer(options);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);
           Task<string> token = GetAPI();
           
        }
        public async Task<string> GetAPI()
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Post, "https://p-spring-cloud-services.uaa.sys.pcf1.vc1.pcf.dell.com/oauth/token");
                request.Content = new FormUrlEncodedContent(new Dictionary<string, string> {
    { "client_id", "p-config-server-00322c35-4b53-4e48-aa71-19a6782bec02" },
    { "client_secret", "3jI6AkvGGKsp" },
    { "grant_type", "client_credentials" }
});
                HttpClient client = new HttpClient();
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var payload = JObject.Parse(await response.Content.ReadAsStringAsync());
                var token = payload.Value<string>("access_token");
                HttpClient client1 = new HttpClient();
                var url = "https://config-950723df-cb21-4741-8c11-28993f849cdd.cfapps.pcf1.vc1.pcf.dell.com/DellDSACustomerDomain/localg1";
                client1.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                var response1 = await client1.GetAsync(url);
                return token;
            }
            catch
            {
                return "";
            }
        }
    }
}
