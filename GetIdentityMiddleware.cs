using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Http;

namespace BroLake.Cloudflare.Identity
{
    public class GetIdentityMiddleware
    {
        private readonly RequestDelegate _next;

        public GetIdentityMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, ICloudflareIdentity identity)
        {
            if (context.Request.Cookies.ContainsKey("CF_Authorization"))
            {
                string cloudflareCookie;
                if (context.Request.Cookies.TryGetValue("CF_Authorization", out cloudflareCookie))
                {
                    if (!string.IsNullOrEmpty(cloudflareCookie))
                    {
                        var handler = new JwtSecurityTokenHandler();
                        var jwtSecurityToken = handler.ReadJwtToken(cloudflareCookie);
                        if (jwtSecurityToken.Issuer == "https://brolakehub.cloudflareaccess.com")
                        {
                            identity.Issuer = jwtSecurityToken.Issuer;

                            object email;
                            object country;

                            if (jwtSecurityToken.Payload.TryGetValue("email", out email))
                            {
                                if (jwtSecurityToken.Payload.TryGetValue("country", out country))
                                {
                                    identity.Email = email as string;
                                    identity.Country = country as string;
                                    await _next(context);
                                    return;
                                }

                                throw new NullReferenceException();
                            }

                            throw new NotAuthenticatedException();
                        }

                        throw new NotAuthenticatedException();
                    }

                    throw new NullReferenceException(nameof(cloudflareCookie));
                }

                throw new NotAuthenticatedException();
            }

            throw new NotAuthenticatedException();
        }
    }
}