using WebApi.Configurations;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.InitializerWebApplicationBuilder();
builder.AddWebApplicationBuilder_Telemetry();
builder.AddWebApplicationBuilder_Authentication();
//builder.AddWebApplicationBuilder_Elmah();
builder.AddWebApplicationBuilder_Swagger();

WebApplication app = builder.Build();
app.InitializerWebApplication();
app.AddWebApplication_Swagger();
app.AddWebApplication_Telemetry();

await app.RunAsync();