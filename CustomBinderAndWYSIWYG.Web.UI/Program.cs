using CustomBinderAndWYSIWYG.Web.UI.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddRazorPages();


builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowSpecificOrigin",
		builder =>
		{
			builder.WithOrigins("https://localhost:7230") // Remplacez par l'URL/port de votre app React
				   .AllowAnyHeader()
				   .AllowAnyMethod();
		});
});

builder.Services.Configure<Keys>(builder.Configuration.GetSection(nameof(Keys)));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();



//var fileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.WebRootPath, "images"));
//var requestPath = "/images";
app.UseCors("AllowSpecificOrigin");
//// Enable displaying browser links.
app.UseStaticFiles();
//app.UseStaticFiles(new StaticFileOptions
//{
//	FileProvider = fileProvider,
//	RequestPath = requestPath
//});

//app.UseDirectoryBrowser(new DirectoryBrowserOptions
//{
//	FileProvider = fileProvider,
//	RequestPath = requestPath
//});

app.UseRouting();
app.UseAuthorization();

app.MapControllers();
app.MapRazorPages();

app.Run();
