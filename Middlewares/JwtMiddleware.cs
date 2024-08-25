using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using UserManagementApp.Services;
using UserManagementApp.Helpers;

namespace UserManagementApp.Middlewares
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly JwtHelper _jwtHelper;

        public JwtMiddleware(RequestDelegate next, JwtHelper jwtHelper)
        {
            _next = next;
            _jwtHelper = jwtHelper;
        }

        public async Task Invoke(HttpContext context, IUserService userService)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
                AttachUserToContext(context, userService, token);

            await _next(context);
        }

        private void AttachUserToContext(HttpContext context, IUserService userService, string token)
        {
            try
            {
                var principal = _jwtHelper.ValidateJwtToken(token);
                if (principal == null)
                    return;

                var userId = principal.Claims.First(x => x.Type == "id").Value;
                var user = userService.GetUserByIdAsync(new System.Guid(userId)).Result;

                context.Items["User"] = user;
            }
            catch
            {
                // Если проверка токена не удалась, ничего не делаем
            }
        }
    }
}
