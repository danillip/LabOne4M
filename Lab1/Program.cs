namespace Lab1
{
    internal partial class Program
    {
        static float Eps = 0.0001f;
        static void PrintMassive(float[] arr) => Console.WriteLine(string.Join(' ', arr));
        static void PrintMatrix(int n, float[] a, float[] b, float[] c, float[] firstString, float[] secondString)
        {
            PrintMassive(firstString);
            PrintMassive(secondString);

            for (int i = 2; i < n - 1; ++i)
            {
                for (int j = 0; j < i - 1; ++j)
                    Console.Write("0 ");
                Console.Write($"{a[i - 2]} ");
                Console.Write($"{b[i - 2]} ");
                Console.Write($"{c[i - 2]} ");
                for (int j = i + 1; j < n - 1; ++j)
                    Console.Write("0 ");
                Console.WriteLine();
            }

            for (int j = 0; j < n - 2; ++j)
                Console.Write("0 ");
            Console.Write($"{a[n - 3]} ");
            Console.Write($"{b[n - 3]}");
            Console.WriteLine();
        }


        static void Main(string[] args)
        {
            using (FileStream fs = new FileStream("Matrix.txt", FileMode.Open))
            {
                using (StreamReader sw = new StreamReader(fs))
                {

                    int n = int.Parse(sw.ReadLine() ?? "0");
                    Console.WriteLine(n);

                    float[] results = new float[n];
                    float[] results2 = new float[n];
                    float[] a = new float[n - 2];
                    float[] b = new float[n - 2];
                    float[] c = new float[n - 3];
                    float[] firstString = new float[n];
                    float[] secondString = new float[n];
                    float[] freeMembers = new float[n];

                    float[] aCopy = new float[n - 1];
                    float[] bCopy = new float[n];
                    float[] cCopy = new float[n - 1];
                    float[] firstStringCopy = new float[n];
                    float[] secondStringCopy = new float[n];
                    float[] freeMembersCopy = new float[n];
                    {
                        float[] temp = sw.ReadLine()?.Split(' ').Select(x => float.Parse(x)).ToArray() ?? new float[n];
                        temp.CopyTo(firstString, 0);
                        temp = sw.ReadLine()?.Split(' ').Select(x => float.Parse(x)).ToArray() ?? new float[n];
                        temp.CopyTo(secondString, 0);
                    }

                    for (int i = 2; i < n - 1; ++i)
                    {
                        float[] temp = sw.ReadLine()?.Split(' ').Select(x => float.Parse(x)).ToArray() ?? new float[n];
                        a[i - 2] = temp[i - 1];
                        b[i - 2] = temp[i];
                        c[i - 2] = temp[i + 1];
                    }

                    {
                        float[] temp = sw.ReadLine()?.Split(' ').Select(x => float.Parse(x)).ToArray() ?? new float[n];
                        a[n - 3] = temp[n - 2];
                        b[n - 3] = temp[n - 1];
                    }
                    freeMembers = sw.ReadLine()?.Split(' ').Select(x => float.Parse(x)).ToArray() ?? new float[n];

                    a.CopyTo(aCopy, 0);
                    b.CopyTo(bCopy, 0);
                    c.CopyTo(cCopy, 0);
                    firstString.CopyTo(firstStringCopy, 0);
                    secondString.CopyTo(secondStringCopy, 0);
                    freeMembers.CopyTo(freeMembersCopy, 0);

                    results = GaussModified(n, aCopy, bCopy, cCopy, firstStringCopy, secondStringCopy, freeMembersCopy);
                    PrintMatrix(n, aCopy, bCopy, cCopy, firstStringCopy, secondStringCopy);
                    Console.WriteLine("\n");
                    PrintMassive(results);
                    Console.WriteLine("\n\n");

                    a.CopyTo(aCopy, 0);
                    b.CopyTo(bCopy, 0);
                    c.CopyTo(cCopy, 0);
                    firstString.CopyTo(firstStringCopy, 0);
                    secondString.CopyTo(secondStringCopy, 0);
                    freeMembers.CopyTo(freeMembersCopy, 0);
                    freeMembersCopy[0] = firstString.Sum();
                    freeMembersCopy[1] = secondString.Sum();
                    for (int i = 2; i < n - 1; ++i)
                        freeMembersCopy[i] = a[i - 2] + b[i - 2] + c[i - 2];
                    freeMembersCopy[n - 1] = a[n - 3] + b[n - 3];
                    PrintMatrix(n, aCopy, bCopy, cCopy, firstStringCopy, secondStringCopy);
                    results2 = GaussModified(n, aCopy, bCopy, cCopy, firstStringCopy, secondStringCopy, freeMembersCopy);
                    PrintMassive(results2);
                    Console.WriteLine(results2.Select((x) => Math.Abs(x - 1)).ToArray().Max());

                    ConductExperiment(10, 10);
                    //ConductExperiment(10, 100);
                    //ConductExperiment(10, 1000);
                    //ConductExperiment(100, 10);
                    //ConductExperiment(100, 100);
                    //ConductExperiment(100, 1000);
                    //ConductExperiment(1000, 10);
                    //ConductExperiment(1000, 100);
                    //ConductExperiment(1000, 1000);
                }
            }
        }
    }
}

