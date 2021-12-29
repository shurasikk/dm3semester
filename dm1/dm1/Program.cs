using System;
using System.IO;

public class CombObj
{
    public int[] obj;
    public int n;
    public char[] alphabet;
    public int k;
    public CombObj(int n, int k) 
    {
        this.n = n;
        this.k = k;
        obj = new int[k];
        alphabet = new char[n];
    }
    public void SetAlphabet(char a, int i)
    {
        alphabet[i] = a;
    }
    void Swap(int i, int j)
    {
        int temp = obj[i];
        obj[i] = obj[j];
        obj[j] = temp;
    }
}

public class PermWithRep : CombObj
{
    public PermWithRep(int n, int k): base(n,k)
    {
        this.n = n;
        this.k = k;
        obj = new int[n];
        alphabet = new char[n];
    }
    public void NextPWR() 
    {
        for (int i = this.k - 1; i > -1; i--)
        {
            if (obj[i] == this.n - 1)
            {
                obj[i] = 0;
                continue;
            }
            obj[i]++;
            break;
        }
    }
    public bool IsLastPWR()  
    {
        for (int i = 0; i < this.k; i++)
        {
            if (obj[i] != this.n - 1)
            {
                return false;
            }
        }
        return true;
    }
}

namespace dm1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите n:");
            int n = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите k:");
            int k = Convert.ToInt32(Console.ReadLine());
            PermWithRep permwithrep_obj = new PermWithRep(n, k);
            for(int i=0; i<n; i++)
            {
                Console.WriteLine("Введите символ алфавита:");
                char a = Convert.ToChar(Console.ReadLine());
                permwithrep_obj.SetAlphabet(a,i);
            }
            for(int i=0; i<k; i++)
            {
                permwithrep_obj.obj[i] = 0;
            }
            StreamWriter countspwr = new StreamWriter(@"C:\Users\Asus\Documents\GitHub\dm3semester\dm1\dm1\task1.txt");
            countspwr.WriteLine("Все перестановки с повторениями из {0} по {1} ", n,k);
            countspwr.Write("Алфавит: {");
            for (int i = 0; i < n; i++)
            {
               countspwr.Write(permwithrep_obj.alphabet[i] + ", ");
            }
            countspwr.Write("}\n");
            while (!permwithrep_obj.IsLastPWR()) 
            {
                for (int i = 0; i < k; i++)
                {
                    countspwr.Write(permwithrep_obj.alphabet[permwithrep_obj.obj[i]]);
                }
                countspwr.WriteLine();
                permwithrep_obj.NextPWR();
            }
            for (int i = 0; i < k; i++)
            {
                countspwr.Write(permwithrep_obj.alphabet[permwithrep_obj.obj[i]]);
            }
            countspwr.Close();
        }
    }
}