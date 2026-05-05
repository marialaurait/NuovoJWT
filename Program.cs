using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication().AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true, //significa che se issuer == la stringa che definisco allora puo passare
        ValidateAudience = true,
        ValidateLifetime = true,//metto una scadenza al token
        ValidateIssuerSigningKey = true,//
        ValidIssuer = "mio sito",// è il sito che genera il token jwt
        ValidAudience = "indirizzo api",//chi consuma, quindi le nostre api
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes("corso_talentform!corso_talentform")//dobbiamo superare i 256 bit. OGNI 8 BIT SONO 1 BYTE QUINDI 1 CARATTERE!!!!!
            )
    };
});


var app = builder.Build();

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
