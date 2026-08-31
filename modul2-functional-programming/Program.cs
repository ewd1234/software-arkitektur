using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;

Person[] people = new Person[]
{
    new Person { Name = "Jens Hansen", Age = 45, Phone = "+4512345678" },
    new Person { Name = "Jane Olsen", Age = 22, Phone = "+4543215687" },
    new Person { Name = "Tor Iversen", Age = 35, Phone = "+4587654322" },
    new Person { Name = "Sigurd Nielsen", Age = 31, Phone = "+4512345673" },
    new Person { Name = "Viggo Nielsen", Age = 28, Phone = "+4543217846" },
    new Person { Name = "Rosa Jensen", Age = 23, Phone = "+4543217846" },
};

//Opgave1
System.Console.WriteLine("Opgave 1:");
System.Console.WriteLine("Samlet sum:");
// Udregner den samlede alder for alle mennesker.
var samletSum = people.Sum(x => x.Age);
Console.WriteLine(string.Join(", ", samletSum));

System.Console.WriteLine("\nNielsen Count");
// Tæller hvor mange der hedder "Nielsen"
var NielsenCount = people.Count(x => x.Name.Contains("Nielsen"));
System.Console.WriteLine(string.Join( "," , NielsenCount));

System.Console.WriteLine("\nOldest person:");
// Find den ældste person
var OldestPerson = people.Max(x => x.Age);
System.Console.WriteLine(string.Join(",", OldestPerson));


//Opgave2
System.Console.WriteLine("\nOpgave 2: ");
System.Console.WriteLine("Skriv person med tlf 4543215687:");
//Find og udskriv personen med mobilnummer “+4543215687”.
var SearchNumber = people.Where(x => x.Phone.Contains("+4543215687")).Select(x => x.Name);
System.Console.WriteLine(string.Join(",", SearchNumber));

System.Console.WriteLine("\nSkriv alle over 30");
//Vælg alle som er over 30 og udskriv dem.
var Over30 = people.Where(x => x.Age > 30).Select(x => x.Name);
System.Console.WriteLine(string.Join(",", Over30));

System.Console.WriteLine("\n+45 fjernet");
//Lav et nyt array med de samme personer, men hvor “+45” er fjernet fra alle telefonnumre.
var null45 = people.Select(x => x.Phone.Replace("+45", ""));
System.Console.WriteLine(string.Join(",",  null45));

System.Console.WriteLine("\nString med personer under 30");
//Generér en string med navn og telefonnummer på de personer, der er yngre end 30, adskilt med komma
var YoungerT30 = people.Where(x => x.Age < 30).Select(x => x.Age + " " + x.Name);
System.Console.WriteLine(string.Join(", ", YoungerT30));

//Opgave3
System.Console.WriteLine("\nOpgave 3: ");
//Funktionen skal returnere en ny funktion. Den nye funktion tager en tekst som input, fjerner alle ord der matcher et ord i “words”, 
// og returnerer en tekst hvor ordene er fjernet.
var CreateWordFilterFn = (string[] words) => {
    return (string text) =>
    {
        return string.Join(" ", text.Split(' ').Where(word => !words.Contains(word)));
    };
};

string[] words1 = { "dum", "grim" };
string text1 = "Dette er en dum grim test";

var filter = CreateWordFilterFn(words1);
string result1 = filter(text1);

Console.WriteLine(result1);

var CreateWordReplacerFn = (string[] words, string replacementWord) => {
    return (string text) =>
    {
        return string.Join(" ", text.Split(' ').Select(word => words.Contains(word) ? replacementWord : word));
    };
};

string[] words2 = { "dum", "grim" };
string replacementWord = "kage";
string text2 = "Dette er en dum og grim test";

var replacer = CreateWordReplacerFn(words2, replacementWord);
string result2 = replacer(text2);

Console.WriteLine(result2);


//Opgave4
//Herunder er en klasse der definere en metode til at sortere et array af Person. Algoritmen der er anvendt hedder BubbleSort.
//Ud over at man skal kalde Sort() med et array af Person, skal man også give den en funktion der tager to Person-objekter, 
// og returnerer et tal svarende til om Person1 eller Person2 er størst.


//Skriv en lambda-funktion, der tager to Person som input, og returnerer -1 hvis den første er yngst, 
// 0 hvis de er lige gamle, og 1 hvis nummer to er yngst.
/*var CompareAge = (p1, p2) =>
    p1.Age < p2.Age ? -1 : (p1.Age == p2.Age ? 0 : 1);

BubbleSort.Sort(people, CompareAge);*/

//Opgave4
System.Console.WriteLine("Opgave 4:");

// 1. Lambda til sammeligning af alder
// Returnerer -1 hvis p1 er yngst, 0 hvis lige gamle, og 1 hvis p2 er yngst.
Func<Person, Person, int> compareAge = (p1, p2) => 
{
    return p1.Age - p2.Age;
};
// Prøv igennem BubbleSort og udskriv
BubbleSort.Sort(people, compareAge);
Console.WriteLine("Sorteret efter alder:");
Console.WriteLine(string.Join(", ", people.Select(p => $"{p.Name} ({p.Age})")));

// 2. Lambda til sortering efter navn (alfabetisk)
Func<Person, Person, int> compareName = (p1, p2) => 
    string.Compare(p1.Name, p2.Name, StringComparison.OrdinalIgnoreCase);

BubbleSort.Sort(people, compareName);
Console.WriteLine("\nSorteret efter navn:");
Console.WriteLine(string.Join(", ", people.Select(p => p.Name)));

// 3. Lambda til sortering efter telefonnummer
Func<Person, Person, int> comparePhone = (p1, p2) => 
    string.Compare(p1.Phone, p2.Phone, StringComparison.Ordinal);

BubbleSort.Sort(people, comparePhone);
Console.WriteLine("\nSorteret efter telefonnummer:");
Console.WriteLine(string.Join(", ", people.Select(p => $"{p.Name}: {p.Phone}")));





public class BubbleSort
{
    // Bytter om på to elementer i et array
    private static void Swap(Person[] array, int i, int j)
    {
        Person temp = array[i];
        array[i] = array[j];
        array[j] = temp;
    }

    // Laver sortering på array med Bubble Sort. 
    // compareFn bruges til at sammeligne to personer med.
    public static void Sort(Person[] array, Func<Person, Person, int> compareFn)
    {
        for (int i = array.Length - 1; i >= 0; i--)
        {
            for (int j = 0; j <= i - 1; j++)
            {
                // Laver en ombytning, hvis to personer står forkert sorteret
                if (compareFn(array[j], array[j + 1]) > 0)
                {
                    Swap(array, j, j + 1);
                }
            }
        }
    }
}

public class Person {
    public required string Name { get; set; }
    public int Age { get; set; }
    public required string Phone { get; set; }
}

