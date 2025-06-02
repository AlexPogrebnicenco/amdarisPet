var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/apsnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();


//builder.Services.AddDbContext<AppDbContext>(options => 
//options.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=OnlineCoursesPlatformEF;Trusted_Connection=True;TrustServerCertificate=True"));
