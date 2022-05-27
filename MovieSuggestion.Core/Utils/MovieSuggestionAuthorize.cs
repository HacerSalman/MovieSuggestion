using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MovieSuggestion.Data.Enums;
using System.Security.Claims;

namespace MovieSuggestion.Core.Utils
{
    public sealed class MovieAuthorizeAttribute : TypeFilterAttribute
    {
        public MovieAuthorizeAttribute(params Permission.Values[] claim) : base(typeof(MovieAuthorizeFilter))
        {
            Arguments = new object[] { claim };
        }
    }

    public class MovieAuthorizeFilter : IAuthorizationFilter
    {
        readonly Permission.Values[] _claim;

        public MovieAuthorizeFilter(params Permission.Values[] claim)
        {
            _claim = claim;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var IsAuthenticated = context.HttpContext.User.Identity.IsAuthenticated;
            if (IsAuthenticated)
            {
                bool flagClaim = false;
                foreach (var item in _claim)
                {
                    if (context.HttpContext.User.HasClaim(ClaimTypes.Role, item.ToString()))
                        flagClaim = true;
                }
                if (!flagClaim)
                    context.Result = new UnauthorizedResult();

            }
            else
            {
                context.Result = new UnauthorizedResult();

            }
        }
    }
}
