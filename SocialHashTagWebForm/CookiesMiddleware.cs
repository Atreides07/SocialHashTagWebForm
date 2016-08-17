using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Owin;

namespace SocialHashTagWebForm
{
    /// <summary>
    /// Cookie saving middleware.
    /// </summary>
    public class CookiesMiddleware : OwinMiddleware
    {
        public const string COOCKIE_SESSION = "session";

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="next">Next middleware in chain</param>
        public CookiesMiddleware(OwinMiddleware next)
            : base(next) { }

        /// <summary>
        /// Main entry point of middleware.
        /// </summary>
        /// <param name="context">Owin Context</param>
        /// <returns>Task</returns>
        public override Task Invoke(IOwinContext context)
        {
            if (!context.Request.Cookies.Any(c => c.Key == COOCKIE_SESSION))
            {
                context.Response.Headers.Append("Set-Cookie", COOCKIE_SESSION + "=" + Guid.NewGuid() + "; ");
            }

            return Next.Invoke(context);
        }
    }
}
