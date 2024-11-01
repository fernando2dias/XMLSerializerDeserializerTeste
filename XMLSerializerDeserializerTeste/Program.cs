using Microsoft.AspNetCore.Mvc;
using System.Xml.Serialization;
using System;
using XMLSerializerDeserializerTeste;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.WebHost.ConfigureKestrel(options =>
{
    options.AllowSynchronousIO = true;
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapPost("/process-xml", async (HttpRequest request) =>
{

    var serializer = new XmlSerializer(typeof(Person));
    var person = (Person)serializer.Deserialize(request.Body);


    var response = new ResponseData
    {
        Greeting = $"Olá, {person.FirstName} {person.LastName}",
        AgeCategory = person.Age > 18 ? "Adulto" : "Menor"
    };

    var responseSerializer = new XmlSerializer(typeof(ResponseData));
    using var stringWriter = new StringWriter();
    responseSerializer.Serialize(stringWriter, response);

    return Results.Content(stringWriter.ToString(), "application/xml");
})
.WithMetadata(new ConsumesAttribute("application/xml"))
.WithMetadata(new ProducesAttribute("application/xml"));

app.Run();


