using System;
using System.IO;

namespace dm8
{
    class Program
    {
        static void Main(string[] args)
        {
            string graph1 = @"C:\Users\Asus\Documents\GitHub\dm3semester\dm8\dm8\graph1.txt";
            string graph2 = @"C:\Users\Asus\Documents\GitHub\dm3semester\dm8\dm8\graph2.txt";
            string[] matr1;
            string[] matr2;
            matr1 = File.ReadAllLines(graph1);
            matr2 = File.ReadAllLines(graph2);

            bool Aftomorfizm = true;
            for (int i = 0; i < matr1.Length; i++)
                if (String.Compare(matr1[i], matr2[i]) != 0)
                {
                    Aftomorfizm = false;
                    break;
                }
            if (Aftomorfizm)
                Console.WriteLine("Преобразование является автоморфизмом.");
            else
                Console.WriteLine("Преобразование не является автоморфизмом.");
        }
    }
}
