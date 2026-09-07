var builder = WebApplication.CreateBuilder(args);
// Åben op for "CORS" i din API.
// Læs om baggrunden her: https://docs.microsoft.com/en-us/aspnet/core/security/cors?view=aspnetcore-10.0

var AllowCors = "_AllowCors";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: AllowCors, builder => {
        builder.AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

var app = builder.Build();
app.UseCors(AllowCors);

List<string> frugterList = new List<string>
{
    "æble", "banan", "pære", "ananas"
};

String[] frugterArr = new String[]
{
    "æble", "banan", "pære", "ananas"
};

List<Todo> todos = new List<Todo>
{
    new Todo {Id = 1, Done = false, Title = "Opvask"},
    new Todo {Id = 2, Done = false, Title = "Støvesuge"},
    new Todo {Id = 3, Done = false, Title = "Lave mad"}
};

//Hello
app.MapGet("/api/hello/", () => new { Message = "Hello world!" });

app.MapGet("/api/hello/{name}", (string name) => new { Message = $"Hello {name}!"});

app.MapGet("/api/hello/{name}/{age}", (string name, int age) => new { Message = $"Hej {name}, du er {age} år gammel!"});

//Fruit
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

//Todo Tasks
app.MapGet("/api/tasks", () => todos);

app.MapGet("/api/tasks/{id}", (int id) =>
{
    Todo? chosenTask = todos.FirstOrDefault(t => t.Id == id);

    return chosenTask;
});

app.MapPut("/api/tasks/{id}", (int id, Todo updatedTask) =>
{
    Todo? todo = todos.FirstOrDefault(t => t.Id == id);

    if (todo == null)
    {
        return Results.NotFound();
    }

    todo.Done = !todo.Done;
    
    return Results.Ok(todos);

});

app.MapDelete("/api/tasks/{id}", (int id) =>
{
    Todo? todo = todos.FirstOrDefault(t => t.Id == id);

    if (todo == null)
    {
        return Results.NotFound();
    }

    todos.Remove(todo);

    return Results.Ok(todos);
});

app.MapPost("/api/tasks", (Todo task) =>
{
    if (string.IsNullOrEmpty(task.Title))
    {
        return Results.BadRequest("Ikke indsat rigtig task");
    }

    task = new Todo {
        Id = Random.Shared.Next(), 
        Done = false, 
        Title = task.Title
        };

    todos.Add(task);

    return Results.Ok(todos);
});

app.Run();
record Fruit(string name);

public class Todo
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public bool Done { get; set; } = false;
}