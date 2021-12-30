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
    public PermWithRep(int n, int k) : base(n, k)
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
public class PermNoRep : CombObj
{
    public PermNoRep(int n, int k) : base(n, k)
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
    public Combinations(int n, int k) : base(n, k)
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

namespace dm2
{
    class Program
    {
        static void Main(string[] args)
        { 
            //2.1, выбираем места для буквы а, потом размещения с повторениями 
            PermWithRep pwr = new PermWithRep(5, 3);
            pwr.alphabet[0] = 'b';
            pwr.alphabet[1] = 'c';
            pwr.alphabet[2] = 'd';
            pwr.alphabet[3] = 'e';
            pwr.alphabet[4] = 'f';
            for (int i = 0; i < 3; i++)
            {
                pwr.obj[i] = 0;
            }
            Combinations comb = new Combinations(5, 2);
            comb.alphabet[0] = '1';
            comb.alphabet[1] = '2';
            comb.alphabet[2] = '3';
            comb.alphabet[3] = '4';
            comb.alphabet[4] = '5';
            for (int i = 0; i < 2; i++)
            {
                comb.obj[i] = i;
            }
            StreamWriter t1 = new StreamWriter(@"C:\Users\Asus\Documents\GitHub\dm3semester\dm2\dm2\task1.txt");
            do
            {
                do
                {
                    for (int i = 1, k = 0; i < 6; i++) 
                    {
                        if (comb.alphabet[comb.obj[0]] == Convert.ToChar(i)+48 || comb.alphabet[comb.obj[1]] == Convert.ToChar(i)+48)
                        {
                            t1.Write('a');  
                        }
                        else
                        {
                            t1.Write(pwr.alphabet[pwr.obj[k]]);  
                            k++;
                        }
                    }
                    t1.WriteLine();
                    pwr.NextPWR();
                } while (!pwr.IsLastPWR());
            } while (comb.NextComb(2));
            t1.Write("fffaa");
            t1.Close();
            //2.2, также выбираем позиции для а, но размещения теперь без повторений
            PermNoRep pnr = new PermNoRep(5, 3);
            pnr.alphabet[0] = 'b';
            pnr.alphabet[1] = 'c';
            pnr.alphabet[2] = 'd';
            pnr.alphabet[3] = 'e';
            pnr.alphabet[4] = 'f';
            for(int i=0; i<5; i++)
            {
                pnr.obj[i] = i;
            }
            Combinations comb2 = new Combinations(5, 2);
            comb2.alphabet[0] = '1';
            comb2.alphabet[1] = '2';
            comb2.alphabet[2] = '3';
            comb2.alphabet[3] = '4';
            comb2.alphabet[4] = '5';
            for (int i = 0; i < 2; i++)
            {
                comb2.obj[i] = i;
            }
            StreamWriter t2 = new StreamWriter(@"C:\Users\Asus\Documents\GitHub\dm3semester\dm2\dm2\task2.txt");
            do
            {
                for (int i = 0; i < 5; i++)
                {
                    pnr.obj[i] = i;
                }
                do
                {
                    for (int i = 1, k = 0; i < 6; i++)
                    {
                        if (comb2.alphabet[comb2.obj[0]] == Convert.ToChar(i) + 48 || comb2.alphabet[comb.obj[1]] == Convert.ToChar(i) + 48)
                        {
                            t2.Write('a');
                        }
                        else
                        {
                            t2.Write(pnr.alphabet[pnr.obj[k]]);
                            k++;
                        }
                    }
                    t2.WriteLine();
                } while (pnr.NextPNR());
            } while (comb2.NextComb(2));
            t2.Close();
        }
    }
}
