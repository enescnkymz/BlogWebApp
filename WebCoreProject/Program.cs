using CoreLayer.Helpers.Abstract;
using CoreLayer.Helpers.Concrete;
using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DateAccessLayer.Abstract;
using DateAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using WebCoreProject.Hubs;
using EntityLayer.Concrete;
using DateAccessLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using BusinessLayer.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;
using System;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews(options =>
{
	// Bu ayar, nullable olmayan tipler için otomatik "Required" kontrolünü kapatýr.
	// Böylece kontrol tamamen FluentValidation'a geçer.
	options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
});

builder.Services.AddMemoryCache();

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IContactDal, EfContactRepository>();
builder.Services.AddScoped<IContactService, ContactManager>();

builder.Services.AddScoped<INotificationDal, EfNotificationRepository>();
builder.Services.AddScoped<INotificationService, NotificationManager>();

builder.Services.AddScoped<IMessageDal, EfMessageRepository>();
builder.Services.AddScoped<IMessageService, MessageManager>();

builder.Services.AddScoped<ICommentDal, EfCommentRepository>();
builder.Services.AddScoped<ICommentService, CommentManager>();

builder.Services.AddScoped<ICategoryDal, EfCategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryManager>();

builder.Services.AddScoped<IWriterDal, EfWriterRepository>();
builder.Services.AddScoped<IWriterService, WriterManager>();

builder.Services.AddScoped<IDescriptionDal, EfDescriptionRepository>();
builder.Services.AddScoped<IDescriptionService, DescriptionManager>();

builder.Services.AddScoped<IExcelHelper, ExcelHelper>();


//builder.Services.AddDbContext<Context>();
//builder.Services.AddIdentity<AppUser, AppRole>(x =>
//{
//	x.Password.RequireNonAlphanumeric = false;
//	x.Password.RequireUppercase = false;
//	x.Password.RequireLowercase = false;
//	x.Password.RequiredLength = 5;

//})
//	.AddEntityFrameworkStores<Context>()
//	.AddErrorDescriber<TurkishIdentityErrorDescriber>();

builder.Services.AddSession();

builder.Services.AddMvc(config =>                       //config üzerinden mvc nin davranýþýný düzenlememizi saðlar
{
	var policy = new AuthorizationPolicyBuilder()
					.RequireAuthenticatedUser()
					.Build();
	config.Filters.Add(new AuthorizeFilter(policy));
});

builder.Services.AddMvc();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)  //Burasý cookie tabanlý authentication u aktif eder
	.AddCookie(x =>
	{
		x.LoginPath = "/Login/Index";
	}
	);

builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/ErrorPage/Error1", "?code={0}");

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthentication();
app.UseSession();
app.UseRouting();

app.UseAuthorization();
app.MapHub<ChatHub>("/ChatHub");
app.MapControllerRoute(
	name: "areas",
	pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
);
app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Description}/{action=Index}/{id?}");

app.Run();
