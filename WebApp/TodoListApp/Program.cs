using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using TodoListApp.Data;
using TodoListApp.Data.Local;
using TodoListApp.Data.Remote;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<TodoListRemoteDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("TodoListAppContext") ?? throw new InvalidOperationException("Connection string 'TodoListAppContext' not found.")));

//builder.Services.TryAddScoped<TodoListJsonContext>();
builder.Services.TryAddScoped<TodoListAppContextWrapper>();

var customUrl = Environment.GetEnvironmentVariable("CUSTOMURL");
if (customUrl != null)
{
    builder.WebHost.UseUrls(customUrl);
    builder.WebHost.UseKestrel();
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
