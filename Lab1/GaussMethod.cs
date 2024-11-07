namespace Lab1
{
    internal partial class Program
    {
        static float[] GaussTestModified(int n, float[] matrixA, float[] matrixB, float[] matrixC, float[] firstRow, float[] secondRow, float[] freeTerms)
        {
            float[] gaussResults = new float[n];


            if (matrixB[n - 3] == 0)
                throw new Exception();

            matrixA[n - 3] /= matrixB[n - 3];
            freeTerms[n - 1] /= matrixB[n - 3];
            matrixB[n - 3] /= matrixB[n - 3];
            matrixB[n - 4] -= matrixC[n - 4] * matrixA[n - 3];
            freeTerms[n - 2] -= matrixC[n - 4] * freeTerms[n - 1];
            matrixC[n - 4] -= matrixC[n - 4] * matrixB[n - 3];

            firstRow[n - 2] -= firstRow[n - 1] * matrixA[n - 3];
            freeTerms[0] -= firstRow[n - 1] * freeTerms[n - 1];
            firstRow[n - 1] -= firstRow[n - 1] * matrixB[n - 3];

            secondRow[n - 2] -= secondRow[n - 1] * matrixA[n - 3];
            freeTerms[1] -= secondRow[n - 1] * freeTerms[n - 1];
            secondRow[n - 1] -= secondRow[n - 1] * matrixB[n - 3];


            if (Math.Abs(matrixA[n - 3] + matrixB[n - 3] - freeTerms[n - 1]) > Eps)
            {
                Console.WriteLine($"{n - 1} {matrixA[n - 3]} {matrixB[n - 3]} {freeTerms[n - 1]} {matrixA[n - 3] + matrixB[n - 3] - freeTerms[n - 1]}");
                throw new Exception();
            }
            else if (Math.Abs(matrixA[n - 4] + matrixB[n - 4] + matrixC[n - 4] - freeTerms[n - 2]) > Eps)
            {
                Console.WriteLine($"{n - 2} {matrixA[n - 4]} {matrixB[n - 4]} {matrixC[n - 4]} {freeTerms[n - 2]} {matrixA[n - 4] + matrixB[n - 4] + matrixC[n - 4] - freeTerms[n - 2]}");
                throw new Exception();
            }
            else if (Math.Abs(firstRow.Sum() - freeTerms[0]) > Eps)
            {
                Console.WriteLine($"{0} {firstRow.Sum()} {freeTerms[0]} {firstRow.Sum() - freeTerms[0]}");
                throw new Exception();
            }
            else if (Math.Abs(secondRow.Sum() - freeTerms[1]) > Eps)
            {
                Console.WriteLine($"{1} {secondRow.Sum()} {freeTerms[1]} {secondRow.Sum() - freeTerms[1]}");
                throw new Exception();
            }

            for (int i = n - 4; i >= 0; --i)
            {
                matrixA[i] /= matrixB[i];
                matrixC[i] /= matrixB[i];
                freeTerms[i + 2] /= matrixB[i];
                matrixB[i] /= matrixB[i];
                if (i != 0)
                {
                    matrixB[i - 1] -= matrixC[i - 1] * matrixA[i];
                    freeTerms[i + 1] -= matrixC[i - 1] * freeTerms[i + 2];
                    matrixC[i - 1] -= matrixC[i - 1] * matrixB[i];
                }

                firstRow[i + 1] -= firstRow[i + 2] * matrixA[i];
                freeTerms[0] -= firstRow[i + 2] * freeTerms[i + 2];
                firstRow[i + 2] -= firstRow[i + 2] * matrixB[i];

                secondRow[i + 1] -= secondRow[i + 2] * matrixA[i];
                freeTerms[1] -= secondRow[i + 2] * freeTerms[i + 2];
                secondRow[i + 2] -= secondRow[i + 2] * matrixB[i];


                if (Math.Abs(matrixA[i] + matrixB[i] - freeTerms[i + 2]) > Eps)
                {
                    Console.WriteLine($"{i + 2} {matrixA[i]} {matrixB[i]} {freeTerms[i + 2]} {matrixA[i] + matrixB[i] - freeTerms[i + 2]}");
                    throw new Exception();
                }
                else if (i != 0 && Math.Abs(matrixA[i - 1] + matrixB[i - 1] + matrixC[i - 1] - freeTerms[i + 1]) > Eps)
                {
                    Console.WriteLine($"{i + 1} {matrixA[i - 1]} {matrixB[i - 1]} {matrixC[i - 1]} {freeTerms[i + 1]} {matrixA[i - 1] + matrixB[i - 1] + matrixC[i - 1] - freeTerms[i + 1]}");
                    throw new Exception();
                }
                else if (Math.Abs(firstRow.Sum() - freeTerms[0]) > Eps)
                {
                    Console.WriteLine($"{0} {firstRow.Sum()} {freeTerms[0]} {firstRow.Sum() - freeTerms[0]}");
                    throw new Exception();
                }
                else if (Math.Abs(secondRow.Sum() - freeTerms[1]) > Eps)
                {
                    Console.WriteLine($"{1} {secondRow.Sum()} {freeTerms[1]} {secondRow.Sum() - freeTerms[1]}");
                    throw new Exception();
                }
            }

            secondRow[0] /= secondRow[1];
            freeTerms[1] /= secondRow[1];
            secondRow[1] /= secondRow[1];
            firstRow[0] -= secondRow[0] * firstRow[1];
            freeTerms[0] -= freeTerms[1] * firstRow[1];
            firstRow[1] -= secondRow[1] * firstRow[1];
            freeTerms[0] /= firstRow[0];
            firstRow[0] /= firstRow[0];
            if (Math.Abs(firstRow.Sum() - freeTerms[0]) > Eps)
            {
                Console.WriteLine($"{100} {firstRow.Sum()} {freeTerms[0]} {firstRow.Sum() - freeTerms[0]}");
                throw new Exception();
            }
            else if (Math.Abs(secondRow.Sum() - freeTerms[1]) > Eps)
            {
                Console.WriteLine($"{111} {secondRow.Sum()} {freeTerms[1]} {secondRow.Sum() - freeTerms[1]}");
                throw new Exception();
            }
            gaussResults[0] = freeTerms[0];
            gaussResults[1] = freeTerms[1] - secondRow[0] * gaussResults[0];
            for (int i = 1; i < n - 1; ++i)
            {
                gaussResults[i + 1] = freeTerms[i + 1] - matrixA[i - 1] * gaussResults[i];
            }
            return gaussResults;
        }
        static float[] GaussModified(int n, float[] matrixA, float[] matrixB, float[] matrixC, float[] firstRow, float[] secondRow, float[] freeTerms)
        {
            float[] gaussResults = new float[n];

            if (matrixB[n - 3] == 0)
                throw new Exception("Деление на ноль");

            matrixA[n - 3] /= matrixB[n - 3];
            freeTerms[n - 1] /= matrixB[n - 3];
            matrixB[n - 3] /= matrixB[n - 3];
            matrixB[n - 4] -= matrixC[n - 4] * matrixA[n - 3];
            freeTerms[n - 2] -= matrixC[n - 4] * freeTerms[n - 1];
            matrixC[n - 4] -= matrixC[n - 4] * matrixB[n - 3];

            firstRow[n - 2] -= firstRow[n - 1] * matrixA[n - 3];
            freeTerms[0] -= firstRow[n - 1] * freeTerms[n - 1];
            firstRow[n - 1] -= firstRow[n - 1] * matrixB[n - 3];

            secondRow[n - 2] -= secondRow[n - 1] * matrixA[n - 3];
            freeTerms[1] -= secondRow[n - 1] * freeTerms[n - 1];
            secondRow[n - 1] -= secondRow[n - 1] * matrixB[n - 3];

            // Вывод текущей матрицы
            //PrintCurrentMatrix(n, matrixA, matrixB, matrixC, firstRow, secondRow);

            for (int i = n - 4; i >= 0; --i)
            {
                matrixA[i] /= matrixB[i];
                matrixC[i] /= matrixB[i];
                freeTerms[i + 2] /= matrixB[i];
                matrixB[i] /= matrixB[i];

                if (i != 0)
                {
                    matrixB[i - 1] -= matrixC[i - 1] * matrixA[i];
                    freeTerms[i + 1] -= matrixC[i - 1] * freeTerms[i + 2];
                    matrixC[i - 1] -= matrixC[i - 1] * matrixB[i];
                }

                firstRow[i + 1] -= firstRow[i + 2] * matrixA[i];
                freeTerms[0] -= firstRow[i + 2] * freeTerms[i + 2];
                firstRow[i + 2] -= firstRow[i + 2] * matrixB[i];

                secondRow[i + 1] -= secondRow[i + 2] * matrixA[i];
                freeTerms[1] -= secondRow[i + 2] * freeTerms[i + 2];
                secondRow[i + 2] -= secondRow[i + 2] * matrixB[i];

                // Вывод текущей матрицы на каждом шаге
                //PrintCurrentMatrix(n, matrixA, matrixB, matrixC, firstRow, secondRow);
            }

            secondRow[0] /= secondRow[1];
            freeTerms[1] /= secondRow[1];
            secondRow[1] /= secondRow[1];
            firstRow[0] -= secondRow[0] * firstRow[1];
            freeTerms[0] -= freeTerms[1] * firstRow[1];
            firstRow[1] -= secondRow[1] * firstRow[1];
            freeTerms[0] /= firstRow[0];
            firstRow[0] /= firstRow[0];

            gaussResults[0] = freeTerms[0];
            gaussResults[1] = freeTerms[1] - secondRow[0] * gaussResults[0];

            for (int i = 1; i < n - 1; ++i)
            {
                gaussResults[i + 1] = freeTerms[i + 1] - matrixA[i - 1] * gaussResults[i];
            }

            return gaussResults;
        }
        static void PrintCurrentMatrix(int n, float[] matrixA, float[] matrixB, float[] matrixC, float[] firstRow, float[] secondRow)
        {
            // Выводим текущую матрицу
            for (int i = 0; i < n - 2; i++)
            {
                for (int j = 0; j < i; j++)
                    Console.Write("0 ");
                Console.Write($"{matrixA[i]:F7} {matrixB[i]:F7} {matrixC[i]:F7} ");
                Console.WriteLine();
            }

            // Выводим последнюю строку
            for (int j = 0; j < n - 2; j++)
                Console.Write("0 ");
            Console.WriteLine($"{matrixA[n - 3]:F7} {matrixB[n - 3]:F7}");

            // Выводим строки firstRow и secondRow
            Console.WriteLine("First row: " + string.Join(" ", firstRow.Select(x => x.ToString("F7"))));
            Console.WriteLine("Second row: " + string.Join(" ", secondRow.Select(x => x.ToString("F7"))));

            // Печатаем разделитель для лучшей читаемости
            Console.WriteLine(new string('-', 30));
        }

    }
}
