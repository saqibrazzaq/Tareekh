using common;
using ui.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.ConfigureEnvironmentVariables();
builder.Services.ConfigureSqlContext();
builder.Services.ConfigureAutoMapper();
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    builder.Services.ConfigureEnvironmentVariables();
}

builder.Services.MigrateDatabase();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture(Constants.DefaultLanguage)
    //.AddSupportedCultures(Constants.AllLanguages.ToArray())
    //.AddSupportedUICultures(Constants.AllLanguages.ToArray())
    ;
app.UseRequestLocalization(localizationOptions);

app.Run();
