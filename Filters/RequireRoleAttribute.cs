using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MVc.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class RequireRoleAttribute : Attribute, IAuthorizationFilter
    {
        private readonly string _role;


        public RequireRoleAttribute(string role)
        {
            _role = role;
        }


        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var hasRole = false;
            if (context.HttpContext.Request.Headers.TryGetValue("X-User-Role", out var values))
            {
                hasRole = string.Equals(values.ToString(), _role, StringComparison.OrdinalIgnoreCase);
            }


            if (!hasRole)
            {
                context.Result = new ForbidResult();
            }
        }
    }
}
