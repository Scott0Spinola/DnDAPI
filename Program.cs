
using DnDAPI.EndPoints;
using DnDAPI.Data;




var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddOpenApiDocument(config =>
{
    config.DocumentName = "DndAPI";
    config.Title = "DndAPI v1";
    config.Version = "v1";
});

builder.Services.AddValidation();
builder.AddDndStoreDb();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi(config =>
    {
        config.DocumentTitle = "DnDAPI";
        config.Path = "/swagger";
        config.DocumentPath = "/swagger/{documentName}/swagger.json";
        config.DocExpansion = "list";
    });
}

app.MapMonstersEndPoints();
app.MigratedDb();

app.Run();

