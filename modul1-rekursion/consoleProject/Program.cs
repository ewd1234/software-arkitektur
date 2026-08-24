// See https://aka.ms/new-console-template for more information
// Program.cs
Console.WriteLine("Opgave 4:");
Console.WriteLine("Del 0: " + Opgave3.Faculty(5)); // Output skal være '120'.
Console.WriteLine("Del 1: " + Opgave4Del1.Sdf(128, 8));
Console.WriteLine("Del 2: " + Opgave4Del2.Potens(4, 4));
Console.WriteLine("Del 3: " + Opgave4Del3.Multiply(25, 2));
Console.WriteLine("Del 4: " + Opgave4Del4.Reverse("EGAKNANAB"));
//Console.WriteLine("Del 5: " + Opgave4Del5.Counter(10));
class Opgave3
{
    public static int Faculty(int n)
    {
        if (n == 0)
        {
            return 1;
        }
        else
        {
            return n * Faculty(n - 1);
        }
    }
}   

class Opgave4Del1
{
    public static int Sdf(int a, int b)
    {
        if (b <= a && a % b == 0)
        {
            return b;
        }

        else if(a < b)
        {
            return Sdf(b, a);
        }

        else
        {
           return Sdf(b, a % b);
        }
    }
}

class Opgave4Del2
{
    public static int Potens(int n, int p)
    {
        if (p <= 0)
        {
            return 1;
        }

        else
        {
            return Potens(n, p - 1) * n;
        }
    }
}

class Opgave4Del3
{
    public static int Multiply(int a, int b)
    {
        if (a == 1)
        {
            return b;
        }

        else if (a == 0)
        {
            return 0;
        }

        else
        {
            return Multiply(a - 1, b) + b;
        }
    }
}

class Opgave4Del4
{
    public static string Reverse(string s)
    {
        if (s.Length <= 1)
        {
            return s;
        }

        else
        {
            return s[s.Length - 1] + Reverse(s.Substring(0, s.Length - 1));
        }
    }
}

/*class Opgave4Del5
{
    // Input 'path' er den mappe man starter i
Opgave5.ScanDir("C:\\Users\\Kristian");

class Opgave5 {
    public static void ScanDir(string path) {
        DirectoryInfo dir = new DirectoryInfo(path);
        FileInfo[] files = dir.GetFiles();

        // Udskriver alle filerne
        foreach (FileInfo file in files) {
            Console.WriteLine(file.Name);
        }
        DirectoryInfo[] dirs = dir.GetDirectories();

        // Kalder rekursivt på alle undermapper
        foreach (DirectoryInfo subdir in dirs) {
            ScanDir(subdir.FullName);
        }        
    }
}
}*/
