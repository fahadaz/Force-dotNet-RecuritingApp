using Microsoft.AspNet.Builder;
using Shared.Util;

namespace Shared
{
	public static class BuilderExtensions
	{
		public static IApplicationBuilder UseKestrelWorkaround(this IApplicationBuilder app)
		{
			return app.UseMiddleware<KestrelWorkaround>();
		}
	}
}
