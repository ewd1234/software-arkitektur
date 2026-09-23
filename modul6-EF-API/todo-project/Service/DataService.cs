using Microsoft.EntityFrameworkCore;
<<<<<<< HEAD
using System.Text.Json;

=======
>>>>>>> 03f3c04 (Lavet stort set hele opgave 2)
using Data;
using Model;

namespace Service;

public class DataService
{
<<<<<<< HEAD
    private BoardContext boardContext { get; set; }

    public DataService(BoardContext db)
    {
        //this.db = db;
=======
    private BoardContext db { get; }

    public DataService(BoardContext db)
    {
        this.db = db;
>>>>>>> 03f3c04 (Lavet stort set hele opgave 2)
    }
    /// <summary>
    /// Seeder noget nyt data i databasen hvis det er nødvendigt.
    /// </summary>
    
    public void SeedData()
    {
<<<<<<< HEAD
=======
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
>>>>>>> 03f3c04 (Lavet stort set hele opgave 2)
        
    }
}