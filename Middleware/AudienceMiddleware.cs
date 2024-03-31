using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace storeAPI.Middleware
{
    public class AudienceMiddleware
    {
        private readonly RequestDelegate _next;
        public AudienceMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            // Xác định Audience từ request
            var audience = DetermineAudience(context.Request);

            // Lưu Audience vào HttpContext để có thể truy cập ở bất kỳ nơi nào trong ứng dụng
            context.Items["Audience"] = audience;

            await _next(context);
        }

        private string DetermineAudience(HttpRequest request)
        {
            var host = request.Host.Host; // Lấy ra chỉ hostname mà không bao gồm cổng

            if (host == "localhost" && request.Host.Port == 5173)
            {
                return "http://localhost:5173";
            }
            else if (host == "localhost" && request.Host.Port == 5174)
            {
                return "http://localhost:5174";
            }
            else
            {
                // Trường hợp mặc định hoặc không xác định
                //return "http://localhost:5174";
                return "http://localhost:1111";
            }
        }
    }
}