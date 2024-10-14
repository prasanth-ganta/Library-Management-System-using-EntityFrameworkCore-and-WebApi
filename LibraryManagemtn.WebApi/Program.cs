using LibraryManagement.Database.Interfaces;
using LibraryManagement.Services.Interfaces;
using LibraryManagement.Database.Implementations;
using LibraryManagementSystem.Services;
using LibraryManagement.Database.DataContext;
using LibraryManagement.Services.Mappers;
using Microsoft.EntityFrameworkCore;
using LibraryManagement.WebApi.Filters;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<BookDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("RepositoryConnection"));
});

builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IBookService, BookService>();

builder.Services.AddAutoMapper(typeof(BookMapper));

builder.Services.AddControllers(options =>
{
    options.Filters.Add<GlobalExceptionFilter>(); 
});

builder.Services.AddScoped<GlobalExceptionFilter>();


var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
