using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    internal partial class Program
    {
        static void ConductExperiment(int n, float range)
        {
            Random random = new Random();

            float[] experimentResults;
            float[] experimentResults2;
            float[] experimentResults3;
            float[] matrixA = new float[n - 1];
            float[] matrixB = new float[n];
            float[] matrixC = new float[n - 1];
            float[] firstRow = new float[n];
            float[] secondRow = new float[n];
            float[] freeTerms = new float[n];
            float[] randomValues = new float[n];

            float[] aCopy = new float[n - 1];
            float[] bCopy = new float[n];
            float[] cCopy = new float[n - 1];
            float[] firstStringCopy = new float[n];
            float[] secondStringCopy = new float[n];
            float[] freeMembersCopy = new float[n];

            // Генерация случайных данных для строк
            {
                float[] temp = new float[n];
                temp = temp.Select(x => (float)(random.NextDouble() * range * 2) - range).ToArray();
                temp.CopyTo(firstRow, 0);
                temp = temp.Select(x => (float)(random.NextDouble() * range * 2) - range).ToArray();
                temp.CopyTo(secondRow, 0);
            }

            // Генерация случайных данных для матриц
            for (int i = 2; i < n - 1; ++i)
            {
                matrixA[i - 2] = (float)(random.NextDouble() * range * 2) - range;
                matrixB[i - 2] = (float)(random.NextDouble() * range * 2) - range;
                matrixC[i - 2] = (float)(random.NextDouble() * range * 2) - range;
            }

            {
                matrixA[n - 3] = (float)(random.NextDouble() * range * 2) - range;
                matrixB[n - 3] = (float)(random.NextDouble() * range * 2) - range;
            }
            freeTerms = freeTerms.Select(x => (float)(random.NextDouble() * range * 2) - range).ToArray();

            // Копирование данных для каждого эксперимента
            matrixA.CopyTo(aCopy, 0);
            matrixB.CopyTo(bCopy, 0);
            matrixC.CopyTo(cCopy, 0);
            firstRow.CopyTo(firstStringCopy, 0);
            secondRow.CopyTo(secondStringCopy, 0);
            freeTerms.CopyTo(freeMembersCopy, 0);

            // Первый эксперимент
            experimentResults = GaussModified(n, aCopy, bCopy, cCopy, firstStringCopy, secondStringCopy, freeMembersCopy);
            Console.WriteLine("Эксперимент 1:");
            Console.WriteLine("Результаты x: " + string.Join(", ", experimentResults));
            Console.WriteLine("Свободные члены после решения: " + string.Join(", ", freeMembersCopy));

            // Второй эксперимент
            matrixA.CopyTo(aCopy, 0);
            matrixB.CopyTo(bCopy, 0);
            matrixC.CopyTo(cCopy, 0);
            firstRow.CopyTo(firstStringCopy, 0);
            secondRow.CopyTo(secondStringCopy, 0);
            freeTerms.CopyTo(freeMembersCopy, 0);

            freeMembersCopy[0] = firstRow.Sum();
            freeMembersCopy[1] = secondRow.Sum();
            for (int i = 2; i < n - 1; ++i)
                freeMembersCopy[i] = matrixA[i - 2] + matrixB[i - 2] + matrixC[i - 2];
            freeMembersCopy[n - 1] = matrixA[n - 3] + matrixB[n - 3];
            experimentResults2 = GaussModified(n, aCopy, bCopy, cCopy, firstStringCopy, secondStringCopy, freeMembersCopy);
            Console.WriteLine("\nЭксперимент 2:");
            Console.WriteLine("Результаты x: " + string.Join(", ", experimentResults2));
            Console.WriteLine("Свободные члены после решения: " + string.Join(", ", freeMembersCopy));

            // Третий эксперимент
            randomValues = randomValues.Select(x => (float)(random.NextDouble() * range * 2) - range).ToArray();
            matrixA.CopyTo(aCopy, 0);
            matrixB.CopyTo(bCopy, 0);
            matrixC.CopyTo(cCopy, 0);
            firstRow.CopyTo(firstStringCopy, 0);
            secondRow.CopyTo(secondStringCopy, 0);
            freeTerms.CopyTo(freeMembersCopy, 0);

            freeMembersCopy[0] = 0;
            freeMembersCopy[1] = 0;
            for (int i = 0; i < n; ++i)
            {
                freeMembersCopy[0] += firstRow[i] * randomValues[i];
                freeMembersCopy[1] += secondRow[i] * randomValues[i];
            }

            for (int i = 2; i < n - 1; ++i)
                freeMembersCopy[i] = matrixA[i - 2] * randomValues[i - 1] + matrixB[i - 2] * randomValues[i] + matrixC[i - 2] * randomValues[i + 1];
            freeMembersCopy[n - 1] = matrixA[n - 3] * randomValues[n - 2] + matrixB[n - 3] * randomValues[n - 1];
            experimentResults3 = GaussModified(n, aCopy, bCopy, cCopy, firstStringCopy, secondStringCopy, freeMembersCopy);
            Console.WriteLine("\nЭксперимент 3:");
            Console.WriteLine("Результаты x: " + string.Join(", ", experimentResults3));
            Console.WriteLine("Свободные члены после решения: " + string.Join(", ", freeMembersCopy));

            // Вычисление ошибок
            Console.WriteLine("\nМаксимальная ошибка для эксперимента 2: " + experimentResults2.Select((x) => Math.Abs(x - 1)).ToArray().Max());
            int tt = -1;
            float q = 0.0000001f;
            Console.WriteLine("Максимальная ошибка для эксперимента 3: " + experimentResults3.Select((x) =>
            {
                ++tt;
                if (Math.Abs(randomValues[tt]) > q)
                    return Math.Abs((x - randomValues[tt]) / randomValues[tt]);
                else
                    return Math.Abs(x - randomValues[tt]);
            }).ToArray().Max());
        }
    }
}
