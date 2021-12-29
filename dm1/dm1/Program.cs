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
    public void Swap(int i, int j)
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
public class Permutations : CombObj
{
    public Permutations(int n, int k) : base(n, k)
    {
        this.n = n;
        this.k = k;
        obj = new int[n];
        alphabet = new char[n];
    }
    public bool NextPerm()
    {
        int i = this.n - 2;
        for (; i >= 0; i--)
        {
            if (obj[i] >= obj[i + 1]) continue;
            else break;
        }    
        if (i == -1)
        {
            return false;
        }
        int j = this.n - 1;
        for(; j>=0;j--)
        {
            if (obj[i] >= obj[j])
                continue;
            else break;
        }
        Swap(i, j);
        int c = i + 1;
        int r = this.n - 1;
        while (c < r)
        {
            Swap(c++, r--);
        }
        return true;
    }
}

public class PermNoRep : CombObj
{
    public PermNoRep(int n, int k): base(n,k)
    {
        this.n = n;
        this.k = k;
        obj = new int[n];
        alphabet = new char[n];
    }
    public bool NextPNR()
    {
        int i;
        do
        {
            i = this.n - 2;
            for (; i >= 0; i--)
            {
                if (obj[i] >= obj[i + 1]) continue;
                else break;
            }
            if (i == -1)
            {
                return false;
            }
            int j = this.n - 1;
            for (; j >= 0; j--)
            {
                if (obj[i] >= obj[j])
                    continue;
                else break;
            }
            Swap(i, j);
            int c = i + 1;
            int r = this.n - 1;
            while (c < r)
            {
                Swap(c++, r--);
            }
        }
        while (i > this.k - 1);
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
            Permutations permut_obj = new Permutations(n, n);
            PermNoRep pnr_obj = new PermNoRep(n, k);
            for(int i=0; i<n; i++)
            {
                Console.WriteLine("Введите символ алфавита:");
                char a = Convert.ToChar(Console.ReadLine());
                permwithrep_obj.SetAlphabet(a,i);
                permut_obj.SetAlphabet(a, i);
                pnr_obj.SetAlphabet(a, i);
            }
            //Построение размещений с повторениями по k элементов
            for(int i=0; i<k; i++)
            {
                permwithrep_obj.obj[i] = 0;
            }
            StreamWriter countspwr = new StreamWriter(@"C:\Users\Asus\Documents\GitHub\dm3semester\dm1\dm1\task1.txt");
            countspwr.WriteLine("Все размещения с повторениями из {0} по {1} ", n,k);
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
            //Построение всех перестановок
            StreamWriter countspermut = new StreamWriter(@"C:\Users\Asus\Documents\GitHub\dm3semester\dm1\dm1\task2.txt");
            countspermut.WriteLine("Все перестановки с {0} элементами ", n);
            countspermut.Write("Алфавит: {");
            for (int i = 0; i < n; i++)
            {
                countspermut.Write(permut_obj.alphabet[i] + ", ");
            }
            countspermut.Write("}\n");
            for (int i = 0; i < n; i++)
            {
                permut_obj.obj[i] = i;
            }
            for (int i = 0; i < n; i++)
            {
                countspermut.Write(permut_obj.alphabet[permut_obj.obj[i]]);
            }
            countspermut.WriteLine();
            while (permut_obj.NextPerm())
            {
                for (int i = 0; i < n; i++)
                {
                    countspermut.Write(permut_obj.alphabet[permut_obj.obj[i]]);
                }
                countspermut.WriteLine();
            }
            countspermut.Close();
            //Построение всех размещений по k
            StreamWriter countsPNR = new StreamWriter(@"C:\Users\Asus\Documents\GitHub\dm3semester\dm1\dm1\task3.txt");
            countsPNR.WriteLine("Все размещения без повторений с {0} элементами по {1}", n,k);
            countsPNR.Write("Алфавит: {");
            for (int i = 0; i < n; i++)
            {
                countsPNR.Write(pnr_obj.alphabet[i] + ", ");
            }
            countsPNR.Write("}\n");
            for (int i = 0; i < n; i++)
            {
                pnr_obj.obj[i] = i;
            }
            for(int i=0; i<k; i++)
            {
                countsPNR.Write(pnr_obj.alphabet[pnr_obj.obj[i]]);
            }
            countsPNR.WriteLine();
            while (pnr_obj.NextPNR()) 
            {
                for (int i = 0; i < k; i++)
                {
                    countsPNR.Write(pnr_obj.alphabet[pnr_obj.obj[i]]);
                }
                countsPNR.WriteLine();
            }
            countsPNR.Close();
            //Построение всех подмножеств
        }
    }
}