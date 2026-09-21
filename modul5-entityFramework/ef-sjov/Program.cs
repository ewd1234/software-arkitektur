using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Model;

using (var db = new TaskContext())
{
    System.Console.WriteLine("================================================");
    Console.WriteLine($"Database path: {db.DbPath}.");
    
    Console.Clear();

    //Create User
    System.Console.WriteLine("Opret bruger? (0 or 1)");
    if (int.TryParse(Console.ReadLine(), out int createU))
    {
        if (createU == 1)
        {
            System.Console.WriteLine("Navn på nye bruger: ");
            string? newUser = Console.ReadLine();

            db.Add(new User(newUser));
            db.SaveChanges();
        }
    }
    // Create
    System.Console.WriteLine("================================================");
    Console.WriteLine("Indsæt en ny task? (0 or 1)");
     if (int.TryParse(Console.ReadLine(), out int createT))
    {
        if (createT == 1)
        {
            System.Console.WriteLine("Hvilken bruger? (id)");
            var allUsers = db.Users.ToList();
                System.Console.WriteLine("\n--- BRUGERE ---");
                foreach (var user in allUsers)
                {
                    System.Console.WriteLine($"Id: {user.UserId} | Navn: {user.Name}");
                }
            if(int.TryParse(Console.ReadLine(), out int taskUser))
            {
                User? chosenUser = db.Users
                    .FirstOrDefault(u => u.UserId == taskUser);
                
                System.Console.WriteLine("Indtast hvilken task: ");
                string? taskText = Console.ReadLine();

                db.Add(new TodoTask(taskText, "test", false, chosenUser));
                db.SaveChanges(); 
            }
        }
    }

    // Read
    System.Console.WriteLine("================================================");
    System.Console.WriteLine("Se alle brugere eller tasks: \n");

    // Vis menuen første gang
    ReadUserOrTask();

    while (int.TryParse(Console.ReadLine(), out int readTask) && readTask != 0)
    {
        switch (readTask)
        {
            case 1: 
                var allUsers = db.Users.ToList();
                System.Console.WriteLine("\n--- BRUGERE ---");
                foreach (var user in allUsers)
                {
                    System.Console.WriteLine($"Id: {user.UserId} | Navn: {user.Name}");
                }
                break;

                case 2:
                    var allTasks = db.Tasks.Include(t => t.User).ToList();
                    System.Console.WriteLine("\n--- TASKS ---");
                    foreach (var task in allTasks)
                    {
                        string userName = task.User != null ? task.User.Name : "Ingen bruger";
                        System.Console.WriteLine($"Id: {task.TodoTaskId}\n Task: {task.Text}\n User: {userName}\n Category: {task.Category}\n Done: {task.Done}\n");
                    }
                    break;
                
                default:
                    System.Console.WriteLine("Ugyldigt valg, prøv igen.");
                    break;
        }

            ReadUserOrTask();

        }


    //Update
    System.Console.WriteLine("================================================");
    System.Console.WriteLine("Opdater valgt task");
    System.Console.WriteLine("Vil du opdatere en task? (0 or 1)");
    if (int.TryParse(Console.ReadLine(), out int updateTask))
    {
        if (updateTask == 1)
        {
            System.Console.WriteLine("Hvilken task skal opdateres?");

            int id = int.Parse(Console.ReadLine());

            var chosenTask = db.Tasks
                .FirstOrDefault(b => b.TodoTaskId == id);

            System.Console.WriteLine("True or False?");

            bool trueOrFalse = bool.Parse(Console.ReadLine());
            chosenTask?.Done = trueOrFalse;

            await db.SaveChangesAsync();

            System.Console.WriteLine("------------");
            System.Console.WriteLine("Opdateret task:");
            System.Console.WriteLine($"Text: {chosenTask?.Text} \n Id: {chosenTask?.TodoTaskId} \n Done: {chosenTask?.Done}");

        }
    }

    // Delete
    System.Console.WriteLine("================================================");
    System.Console.WriteLine("Slet en user eller task");
    DeleteUserOrTask();

    while (int.TryParse(Console.ReadLine(), out int deleteTask) && deleteTask != 0)
    {
        switch (deleteTask)
        {
            case 1:
                var allUsers = db.Users.ToList();
                System.Console.WriteLine("\n--- BRUGERE ---");
                foreach (var user in allUsers)
                {
                    System.Console.WriteLine($"Id: {user.UserId} | Navn: {user.Name}");
                }

                System.Console.WriteLine("Hvilken user skal fjernes (id)?:");
                if (int.TryParse(Console.ReadLine(), out int removeUserId))
                {
                    var removeUser = db.Users
                        .FirstOrDefault(u => u.UserId == removeUserId);

                    if (removeUser == null)
                    {
                        System.Console.WriteLine("=====");
                        System.Console.WriteLine($"Kunne ikke finde en bruger med ID {removeUserId}.");
                        System.Console.WriteLine("=====");
                        break;
                    }

                    db.Users.Remove(removeUser);
                    db.SaveChangesAsync();

                    System.Console.WriteLine("=====");
                    System.Console.WriteLine($"Bruger med ID: {removeUserId} er nu slettet!");
                    System.Console.WriteLine("=====");
                }
                break;
            
            case 2:
                System.Console.WriteLine("Hvilken task skal fjernes (id)?:");

                if (int.TryParse(Console.ReadLine(), out int removeTaskId))
                {
                    var removetask = db.Tasks
                        .FirstOrDefault(b => b.TodoTaskId == removeTaskId);

                    if (removetask == null)
                    {
                        System.Console.WriteLine("=====");
                        System.Console.WriteLine($"Kunne ikke finde en task med ID {removeTaskId}.");
                        System.Console.WriteLine("=====");
                        break;
                    }

                    db.Tasks.Remove(removetask);
                    db.SaveChangesAsync();

                    System.Console.WriteLine("=====");
                    System.Console.WriteLine($"Task med ID: {removeTaskId} er nu slettet!");
                    System.Console.WriteLine("=====");
                }
                break;
            
            default:
                System.Console.WriteLine("Ugyldigt valg, prøv igen.");
                break;
        }

        DeleteUserOrTask();
    }

}

static void ReadUserOrTask()
{
    System.Console.WriteLine("\n0: Skip / Afslut");
    System.Console.WriteLine("1: Se alle brugere ");
    System.Console.WriteLine("2: Se alle tasks");
    System.Console.Write("Vælg mulighed: ");
}

static void DeleteUserOrTask()
{
    System.Console.WriteLine("\n0: Skip / Afslut");
    System.Console.WriteLine("1: Slet én bruger ");
    System.Console.WriteLine("2: Slet én task");
    System.Console.Write("Vælg mulighed: ");
}