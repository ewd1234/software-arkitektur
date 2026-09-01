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

List<Todo> todos = new List<Todo>
{
    new Todo {Id = 1, IsDone = false, Title = "Opvask"},
    new Todo {Id = 2, IsDone = false, Title = "Støvesuge"},
    new Todo {Id = 3, IsDone = false, Title = "Lave mad"}
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
app.MapGet("/api/tasks", () => new {Message = todos});

app.MapGet("/api/tasks/{id}", (int id) =>
{
    Todo? chosenTask = todos.FirstOrDefault(t => t.Id == id);

    return chosenTask;
});

app.MapPut("/api/tasks/{id}", (int id, Todo updatedTask) =>
{
    if (id < 0 || id >= todos.Count())
    {
        return Results.BadRequest($"Kan ikke finde id: {id}");
    }

    updatedTask = todos[id - 1];
    updatedTask.IsDone = !updatedTask.IsDone;
    
    return Results.Ok(todos);

});

app.MapDelete("/api/tasks/{id}", (int id) =>
{
    if (id <= 0 || id >= todos.Count())
    {
        return Results.BadRequest($"Kan ikke finde id: {id}");
    }

    todos.Remove(todos[id - 1]);

    return Results.Ok(todos);
});

app.MapPost("/api/tasks", (Todo task) =>
{
    if (string.IsNullOrEmpty(task.Title))
    {
        return Results.BadRequest("Ikke indsat rigtig task");
    }

    task = new Todo {Id = task.Id, IsDone = task.IsDone, Title = task.Title};

    todos.Add(task);

    return Results.Ok(todos);
});

app.Run();
record Fruit(string name);

public class Todo
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public bool IsDone { get; set; }
}