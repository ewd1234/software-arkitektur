using Data;
using Service;

using var db = new BoardContext();

var dataService = new DataService(db);
dataService.SeedData();

var boards = dataService.GetBoardsWithTodosAndUsers();

foreach (var board in boards)
{
	Console.WriteLine($"Board {board.boardId}");

	foreach (var todo in board.todos ?? [])
	{
		Console.WriteLine($"- {todo.name} ({todo.category}) - bruger: {todo.user?.name}");
	}
}

System.Console.WriteLine("Opret: \n1: User \n2: Todo \nBoard");