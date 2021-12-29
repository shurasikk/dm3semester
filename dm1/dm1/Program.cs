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
public class Combinations : CombObj
{
    public Combinations(int n, int k) :base(n,k)
    {
        this.n = n;
        this.k = k;
        obj = new int[k];
        alphabet = new char[n];
    }
    public bool NextComb(int k)
    {
        for (int i = k - 1; i >= 0; i--)
            if (obj[i] < this.n - k + i)
            {
                obj[i]++;
                for (int j = i + 1; j < k; j++)
                    obj[j] = obj[j - 1] + 1;
                return true;
            }
        return false;
    }
}
public class CombWithRep: CombObj
{
    public CombWithRep(int n, int k) : base(n, k)
    {
        this.n = n;
        this.k = k;
        obj = new int[k];
        alphabet = new char[n];
    }
    public bool NextCWR()
    {
        int j = this.k - 1;
        while (j >= 0 && obj[j] == this.n - 1) j--;
        if (j < 0) return false;
        if (obj[j] >= this.n - 1)
            j--;
        obj[j]++;
        if (j == this.k - 1) return true;
        for (int k = j + 1; k < this.k; k++)
            obj[k] = obj[j];
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
            Combinations subset = new Combinations(n, n);
            Combinations combinations = new Combinations(n, k);
            CombWithRep cwr = new CombWithRep(n, k);
            for(int i=0; i<n; i++)
            {
                Console.WriteLine("Введите символ алфавита:");
                char a = Convert.ToChar(Console.ReadLine());
                permwithrep_obj.SetAlphabet(a,i);
                permut_obj.SetAlphabet(a, i);
                pnr_obj.SetAlphabet(a, i);
                subset.SetAlphabet(a, i);
                combinations.SetAlphabet(a, i);
                cwr.SetAlphabet(a, i);
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
            StreamWriter countsSubst = new StreamWriter(@"C:\Users\Asus\Documents\GitHub\dm3semester\dm1\dm1\task4.txt");
            countsSubst.WriteLine("Все подмножества множества с {0} элементами ", n);
            countsSubst.Write("Алфавит: {");
            for (int i = 0; i < n; i++)
            {
                countsSubst.Write(subset.alphabet[i] + ", ");
            }
            countsSubst.Write("}\n");
            for (int l = 0; l < n + 1; l++)
            {
                for (int i = 0; i < l; i++) 
                {
                    subset.obj[i] = i;
                }
                for (int i = 0; i < l; i++)
                {
                    countsSubst.Write(subset.alphabet[subset.obj[i]]);
                }
                countsSubst.WriteLine();
                while (subset.NextComb(l))
                {
                    for (int i = 0; i < l; i++)
                    {
                        countsSubst.Write(subset.alphabet[subset.obj[i]]);
                    }
                    countsSubst.WriteLine();
                }
            }
            countsSubst.Write("∅");//добавляем пустое множество
            countsSubst.Close();
            //Построить все сочетания по k элементов.
            StreamWriter countsComb = new StreamWriter(@"C:\Users\Asus\Documents\GitHub\dm3semester\dm1\dm1\task5.txt");
            countsComb.WriteLine("Все сочетания из {0} элементов по {1} ", n, k);
            countsComb.Write("Алфавит: {");
            for (int i = 0; i < n; i++)
            {
                countsComb.Write(subset.alphabet[i] + ", ");
            }
            countsComb.Write("}\n");
            for (int i = 0; i < k; i++) 
            {
                combinations.obj[i] = i;
            }
            for (int i = 0; i < k; i++)
            {
                countsComb.Write(combinations.alphabet[combinations.obj[i]]);
            }
            countsComb.WriteLine();
            while (combinations.NextComb(combinations.k)) 
            {
                for (int i = 0; i < k; i++)
                {
                    countsComb.Write(combinations.alphabet[combinations.obj[i]]);
                }
                countsComb.WriteLine();
            }
            countsComb.Close();
            //Построить все сочетания с повторениями.
            StreamWriter countscwr = new StreamWriter(@"C:\Users\Asus\Documents\GitHub\dm3semester\dm1\dm1\task6.txt");
            countscwr.WriteLine("Все сочетания с повторениями из {0} элементов по {1} ", n, k);
            countscwr.Write("Алфавит: {");
            for (int i = 0; i < n; i++)
            {
                countscwr.Write(subset.alphabet[i] + ", ");
            }
            countscwr.Write("}\n");
            for(int i=0; i<k; i++)
            {
                cwr.obj[i] = 0;
            }
            for (int i = 0; i < k; i++)
            {
                countscwr.Write(cwr.alphabet[cwr.obj[i]]);
            }
            countscwr.WriteLine();
            while(cwr.NextCWR())
            {
                for (int i = 0; i < k; i++)
                {
                    countscwr.Write(cwr.alphabet[cwr.obj[i]]);
                }
                countscwr.WriteLine();
            }
            countscwr.Close();
        }
    }
}