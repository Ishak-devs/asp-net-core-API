using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using projet_aspnet_api;

var builder = WebApplication.CreateBuilder(args);

//Ajout de service controller dans le conteneur
builder.Services.AddControllers();

//ajout du service context dasn le conteneur
builder.Services.AddDbContext<Context>
    (options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MaChaîneDeConnexion")));

//Définir le schéma d'authentification basé sur les cookies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)

    
    .AddCookie(options =>
    {
        options.LoginPath = "/api/Compte/Login"; 
        options.AccessDeniedPath = "/api/Compte/AccessDenied"; 
    });

//builder.Services.AddDistributedMemoryCache();
//builder.Services.AddSession(options =>
//{
//    options.IdleTimeout = TimeSpan.FromMinutes(30);
//    options.Cookie.HttpOnly = true;
//    options.Cookie.IsEssential = true;
//});


//Définir le swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
});

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API v1"));
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

//app.UseSession();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();