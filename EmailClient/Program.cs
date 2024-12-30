using EmailClient;
using EmailClient.Models.ApiServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient<User1ApiServices>(client =>
 {
     client.BaseAddress = new Uri(restUrl.URL);
     client.DefaultRequestHeaders.Add("Accept", "application/json");
 });
 builder.Services.AddHttpClient<UploadFileApiServices>(client =>
 {
     client.BaseAddress = new Uri(restUrl.URL);
     client.DefaultRequestHeaders.Add("Accept", "application/json");
 });
 builder.Services.AddScoped<UploadFileApiServices>();
builder.Services.AddScoped<User1ApiServices>();
builder.Services.AddHttpClient();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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
