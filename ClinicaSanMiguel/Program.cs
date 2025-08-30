using ClinicaSanMiguel.Repositorys.Implementations;
using ClinicaSanMiguel.Repositorys.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IPacienteRepository, PacienteRepository>();
builder.Services.AddScoped<ICitaMedicaRepository, CitaMedicaRepository>();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(option => 
{
    option.IdleTimeout = TimeSpan.FromMinutes(10);
    option.Cookie.HttpOnly = true;
    option.Cookie.IsEssential = true;
});
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
