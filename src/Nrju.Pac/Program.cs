using Microsoft.AspNetCore.StaticFiles;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseDefaultFiles(new DefaultFilesOptions
{
    DefaultFileNames = ["default.pac"]
});

app.UseStaticFiles(new StaticFileOptions
{
    ContentTypeProvider = new FileExtensionContentTypeProvider(
        new Dictionary<string, string>
        {
            [".pac"] = "application/x-ns-proxy-autoconfig"
        })
});

app.Run();

#pragma warning disable CA1515,CS1591

public partial class Program
{ }
