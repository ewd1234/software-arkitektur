using Microsoft.EntityFrameworkCore;
using System.Text.Json;

using Data;
using Model;

namespace Service;

public class DataService
{
    private BoardContext db { get; }

    public DataService(BoardContext db)
    {
        this.db = db;
    }
    /// <summary>
    /// Seeder noget nyt data i databasen hvis det er nødvendigt.
    /// </summary>
    
    public void SeedData()
    {
        if (db.Boards.Any())
        {
            return;
        }

        var board = new Board
        {
            todos = new List<Todo>
            {
                new Todo
                {
                    name = "Lav UML-diagram",
                    category = "Skole",
                    user = new User { name = "Anna" }
                },
                new Todo
                {
                    name = "Lav migration",
                    category = "EF Core",
                    user = new User { name = "Mikkel" }
                }
            }
        };

        db.Boards.Add(board);
        db.SaveChanges();
    }

    public List<Board> GetBoardsWithTodosAndUsers()
    {
        return db.Boards
            .Include(board => board.todos!)
            .ThenInclude(todo => todo.user)
            .ToList();
    }

    public void CreateUser(User user){
        
    }
}