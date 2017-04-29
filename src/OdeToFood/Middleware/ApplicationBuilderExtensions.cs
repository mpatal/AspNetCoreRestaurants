using System.IO;
using Microsoft.Extensions.FileProviders;

// ReSharper disable once CheckNamespace
namespace Microsoft.AspNetCore.Builder
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseNodeModules(this IApplicationBuilder app, string root)
        {
            var nodePath =  Path.Combine(root, "node_modules");
            app.UseStaticFiles(new StaticFileOptions
            {
                RequestPath = "/node_modules",
                FileProvider = new PhysicalFileProvider(nodePath)
                
            });

            return app;
        }
    }
}
