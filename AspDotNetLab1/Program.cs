using AspDotNetLab1;
using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.Extensions.FileProviders;




var builder = WebApplication.CreateBuilder(args);

// Add services to the container.




builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme)
   .AddNegotiate();

builder.Services.AddAuthorization(options =>
{
    // By default, all incoming requests will be authorized according to the default policy.
    options.FallbackPolicy = options.DefaultPolicy;
});
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();



app.UseRouting();
app.UseFileServer();


app.UseEndpoints(endpoints =>
{
    endpoints.MapGet("/", async context =>
    {
        await context.Response.WriteAsync("Hello World!");
    });
    endpoints.MapGet("/home/index", async context =>
    {
        await context.Response.WriteAsync("Hello in home's index file");
    });
    endpoints.MapGet("/home/about", async context =>
    {
        await context.Response.WriteAsync("Hello in home's about file\nNow it clear");
    });
});

app.UseFileServer(new FileServerOptions
{
    EnableDirectoryBrowsing = true,
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"static"))
});

app.UseToken("SecretToken-123");

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
