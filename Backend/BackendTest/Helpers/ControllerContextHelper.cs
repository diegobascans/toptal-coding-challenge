using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackendTest.Helpers
{
    public static class ControllerContextHelper
    {
        public static ControllerContext GetMockControllerContext()
        {
            ControllerContext context = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };

            // UserId = 0
            context.HttpContext.Request.Headers.Add("Authorization", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VySWQiOjB9.-iosKlFD7a39uUPaX7_88R8CtUFtT9_aVUHEXbtbolw");

            return context;
        }
    }
}
