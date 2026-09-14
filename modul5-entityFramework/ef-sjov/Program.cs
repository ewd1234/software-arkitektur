using Microsoft.AspNetCore.Http.HttpResults;
using Model;

using (var db = new TaskContext())
{
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
    Console.WriteLine("Find det sidste task");
    var lastTask = db.Tasks
        .OrderBy(b => b.TodoTaskId)
        .Last();
    Console.WriteLine($"Text: {lastTask.Text} \n Id: {lastTask.TodoTaskId} \n Done: {lastTask.Done} ");

    //Update
    System.Console.WriteLine("\nOpdater valgt task");
    System.Console.WriteLine("Hvilken task skal opdateres? \n");

    int id = int.Parse(Console.ReadLine());

    var chosenTask = db.Tasks
        .FirstOrDefault(b => b.TodoTaskId == id);

    System.Console.WriteLine("True or False?");

    bool trueOrFalse = bool.Parse(Console.ReadLine());
    chosenTask.Done = trueOrFalse;

    await db.SaveChangesAsync();

    System.Console.WriteLine($" \nText: {chosenTask.Text} \n Id: {chosenTask.TodoTaskId} \n Done: {chosenTask.Done}");

    // Delete
    System.Console.WriteLine("\nSlet en task");
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