using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Diagnostics;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Routing;
using Microsoft.Framework.Configuration;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.Logging;
using Microsoft.Framework.Logging.Console;


namespace rapp
{
	public class AppSettings
	{
		public string ConsumerKey { get; set; }
		public string ConsumerSecret { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }		
		public string IsSandboxUser { get; set; }
		public string ProdUrl { get; set; }
		public string SandboxUrl { get; set; }
	}
}