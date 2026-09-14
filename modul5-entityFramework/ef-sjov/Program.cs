using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Model;

using (var db = new TaskContext())
{
    System.Console.WriteLine("================================================");
    Console.WriteLine($"Database path: {db.DbPath}.");
    
    //Create User
    System.Console.WriteLine("Opret bruger? (0 or 1)");
    if (int.TryParse(Console.ReadLine(), out int createU))
    {
        if (createU == 1)
        {
            db.Add(new User("Happer"));
            db.SaveChanges();
        }
    }
    // Create
    System.Console.WriteLine("================================================");
    Console.WriteLine("Indsæt et nyt task? (0 or 1)");
     if (int.TryParse(Console.ReadLine(), out int createT))
    {
        if (createT == 1)
        {
            System.Console.WriteLine("Hvilken bruger? (id)");
            if(int.TryParse(Console.ReadLine(), out int taskUser))
            {
                User chosenUser = db.Users
                    .FirstOrDefault(u => u.UserId == taskUser);
                
                db.Add(new TodoTask("En opgave der skal løses", "test", false, chosenUser));
                db.SaveChanges(); 
            }
        }
    }

    // Read
    System.Console.WriteLine("================================================");
    System.Console.WriteLine("Se alle brugere eller tasks: \n");

    // Vis menuen første gang
    userOrTask();

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

            userOrTask();

        }

    
   /* Console.WriteLine("Find den sidste task");
    var lastTask = db.Tasks
        .OrderBy(b => b.TodoTaskId)
        .Last();
    Console.WriteLine($"Text: {lastTask.Text} \n Id: {lastTask.TodoTaskId} \n Done: {lastTask.Done} ");*/

    //Update
    System.Console.WriteLine("================================================");
    System.Console.WriteLine("\nOpdater valgt task");
    System.Console.WriteLine("Vil du opdatere en task? (0 or 1)");
    if (int.TryParse(Console.ReadLine(), out int updateTask))
    {
        if (updateTask == 1)
        {
            System.Console.WriteLine("Hvilken task skal opdateres? \n");

            int id = int.Parse(Console.ReadLine());

            var chosenTask = db.Tasks
                .FirstOrDefault(b => b.TodoTaskId == id);

            System.Console.WriteLine("True or False?");

            bool trueOrFalse = bool.Parse(Console.ReadLine());
            chosenTask.Done = trueOrFalse;

            await db.SaveChangesAsync();

            System.Console.WriteLine($" \nText: {chosenTask.Text} \n Id: {chosenTask.TodoTaskId} \n Done: {chosenTask.Done}");

        }
    }

    // Delete
    System.Console.WriteLine("================================================");
    System.Console.WriteLine("\nSlet en task");
    System.Console.WriteLine("Vil du slette en task? (0 or 1)");

    if (int.TryParse(Console.ReadLine(), out int deleteTask))
    {
        if (deleteTask == 1)
        {
            System.Console.WriteLine("Hvilken task skal fjernes (id)?:");

            if (int.TryParse(Console.ReadLine(), out int removeid))
            {
                var removetask = db.Tasks
                    .FirstOrDefault(b => b.TodoTaskId == removeid);

                if (removetask == null)
                {
                    System.Console.WriteLine($"Kunne ikke finde en task med ID {removeid}.");
                    return;
                }

                db.Tasks.Remove(removetask);
                db.SaveChangesAsync();

                System.Console.WriteLine($"Du har nu slettet taskId: {removetask.TodoTaskId}");
            }
            else
            {
                System.Console.WriteLine("Ugyldigt ID. Indtast venligst et tal.");
            }
        }
    }


}

static void userOrTask()
{
    System.Console.WriteLine("\n0: Skip / Afslut");
    System.Console.WriteLine("1: Se alle brugere ");
    System.Console.WriteLine("2: Se alle tasks");
    System.Console.Write("Vælg mulighed: ");
}