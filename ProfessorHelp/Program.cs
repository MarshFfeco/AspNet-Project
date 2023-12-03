using HashidsNet;
using ProfessorHelp.Data;
using ProfessorHelp.Filter;
using ProfessorHelp.Middleware;
using ProfessorHelp.Services.Automapper;
using ProfessorHelp.Services.Bootstrapper;
using ProfessorHelp.Shared.Exception.Filter;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRouting(option => option.LowercaseUrls = true);

builder.Services.AddHttpContextAccessor();

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddAplication(builder.Configuration);

builder.Services.AddMvc(option => option.Filters.Add(typeof(ExceptionFilter)));

builder.Services.AddScoped(provider => new AutoMapper.MapperConfiguration(cfg =>
{
    cfg.AddProfile(new MappingProfiles(provider.GetService<IHashids>()));
}).CreateMapper());

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>();

builder.Services.AddScoped<ProfessorAuth>();

var app = builder.Build();

app.UseMiddleware<CultureMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
