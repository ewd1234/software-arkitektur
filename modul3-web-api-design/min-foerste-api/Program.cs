var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

List<string> frugterList = new List<string>
{
    "æble", "banan", "pære", "ananas"
};

String[] frugterArr = new String[]
{
    "æble", "banan", "pære", "ananas"
};

app.MapGet("/api/hello/", () => new { Message = "Hello world!" });

app.MapGet("/api/hello/{name}", (string name) => new { Message = $"Hello {name}!"});

app.MapGet("/api/hello/{name}/{age}", (string name, int age) => new { Message = $"Hej {name}, du er {age} år gammel!"});

app.MapGet("/api/fruit", () => new {Message = frugterArr} );

app.MapGet("/api/fruit/{index}", (int index) => new {Message = frugterArr[index]});

app.MapGet("/api/fruit/random", () => new {Message = frugterArr[Random.Shared.Next(frugterArr.Count())]});

app.MapPost("/api/fruit/list/", (Fruit fruit) =>
{

    if (string.IsNullOrEmpty(fruit.name))
    {
        return Results.BadRequest();
    }

    frugterList.Add(fruit.name);

    Console.WriteLine($"Tilføjet frugt: {fruit.name}");

    return Results.Ok(frugterList);
});

app.MapPost("/api/fruit/arr/", (Fruit fruit) =>
{
    if (string.IsNullOrEmpty(fruit.name))
    {
        return Results.BadRequest();
    }

    frugterArr = frugterArr.Append(fruit.name).ToArray();

    Console.WriteLine($"Tilføjet frugt: {fruit.name}");

    return Results.Ok(frugterArr);
});




app.Run();
record Fruit(string name);