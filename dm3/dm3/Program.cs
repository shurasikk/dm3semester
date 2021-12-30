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
public class CombWithRep : CombObj
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

namespace dm3
{
    class Program
    {
        static void Main(string[] args)
        {
            //3.1
            PermNoRep pnr = new PermNoRep(6, 3);
            pnr.alphabet[0] = 'a';
            pnr.alphabet[1] = 'b';
            pnr.alphabet[2] = 'c';
            pnr.alphabet[3] = 'd';
            pnr.alphabet[4] = 'e';
            pnr.alphabet[5] = 'f';
            for (int i = 0; i < 5; i++)
            {
                pnr.obj[i] = i;
            }
            pnr.n = 5;
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
            StreamWriter sw = new StreamWriter(@"C:\Users\Asus\Documents\GitHub\dm3semester\dm3\dm3\task1.txt");
            for (int p = 0; p < 6; p++) 
            {
                Array.Resize(ref pnr.alphabet, 6);
                pnr.alphabet[0] = 'a';
                pnr.alphabet[1] = 'b';
                pnr.alphabet[2] = 'c';
                pnr.alphabet[3] = 'd';
                pnr.alphabet[4] = 'e';
                pnr.alphabet[5] = 'f';
                char a = pnr.alphabet[p];
                pnr.alphabet[p] = '0';
                //на время удаляем букву которая 2 раза встретилась из алфавита
                Array.Sort(pnr.alphabet);
                Array.Reverse(pnr.alphabet);
                Array.Resize(ref pnr.alphabet, 5);
                Array.Reverse(pnr.alphabet);

                for (int i = 0; i < 2; i++)
                {
                    comb.obj[i] = i;
                }
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
                            if (comb.alphabet[comb.obj[0]] == Convert.ToChar(i) + 48 || comb.alphabet[comb.obj[1]] == Convert.ToChar(i) + 48)
                            {
                                sw.Write(a);
                            }
                            else
                            {
                                sw.Write(pnr.alphabet[pnr.obj[k]]);
                                k++;
                            }
                        }
                        sw.WriteLine();
                    } while (pnr.NextPNR());
                } while (comb.NextComb(2));
            }
            sw.Close();
            //3.2
            PermNoRep pnr2 = new PermNoRep(6, 2);
            Combinations choose_letter = new Combinations(6, 2);
            Combinations places1 = new Combinations(6, 2);
            Combinations places2 = new Combinations(6, 2);
            choose_letter.alphabet[0] = 'a';
            choose_letter.alphabet[1] = 'b';
            choose_letter.alphabet[2] = 'c';
            choose_letter.alphabet[3] = 'd';
            choose_letter.alphabet[4] = 'e';
            choose_letter.alphabet[5] = 'f';

            places1.alphabet[0] = '1';
            places1.alphabet[1] = '2';
            places1.alphabet[2] = '3';
            places1.alphabet[3] = '4';
            places1.alphabet[4] = '5';
            places1.alphabet[5] = '6';
            StreamWriter sw2 = new StreamWriter(@"C:\Users\Asus\Documents\GitHub\dm3semester\dm3\dm3\task2.txt");
            for (int i = 0; i < 2; i++)
            {
                choose_letter.obj[i] = i;
            }
            do
            {
                char a = choose_letter.alphabet[choose_letter.obj[0]];
                char b = choose_letter.alphabet[choose_letter.obj[1]];

                places1.alphabet[0] = '1';
                places1.alphabet[1] = '2';
                places1.alphabet[2] = '3';
                places1.alphabet[3] = '4';
                places1.alphabet[4] = '5';
                places1.alphabet[5] = '6';

                for (int j = 0; j < 2; j++)
                {
                    places1.obj[j] = j;
                }

                Array.Resize(ref pnr2.alphabet, 6);
                pnr2.alphabet[0] = 'a';
                pnr2.alphabet[1] = 'b';
                pnr2.alphabet[2] = 'c';
                pnr2.alphabet[3] = 'd';
                pnr2.alphabet[4] = 'e';
                pnr2.alphabet[5] = 'f';

                for (int j = 0; j < 6; j++)
                {
                    if (pnr2.alphabet[j] == a || pnr2.alphabet[j] == b)
                    {
                        pnr2.alphabet[j] = '0';
                    }
                }

                pnr2.n = 4;
                Array.Sort(pnr2.alphabet); 
                Array.Reverse(pnr2.alphabet);
                Array.Resize(ref pnr2.alphabet, 4);
                Array.Reverse(pnr2.alphabet);

                do
                {
                    Array.Resize(ref places2.alphabet, 6);
                    places2.alphabet[0] = '1';
                    places2.alphabet[1] = '2';
                    places2.alphabet[2] = '3';
                    places2.alphabet[3] = '4';
                    places2.alphabet[4] = '5';
                    places2.alphabet[5] = '6';

                    for (int l = 0; l < 6; l++)
                    {
                        if (places2.alphabet[l] == places1.alphabet[places1.obj[0]] || places2.alphabet[l] == places1.alphabet[places1.obj[1]])
                        {
                            places2.alphabet[l] = '0';
                        }
                    }
                    places2.n = 4;
                    Array.Sort(places2.alphabet);
                    Array.Reverse(places2.alphabet);
                    Array.Resize(ref places2.alphabet, 4);
                    Array.Reverse(places2.alphabet);
                    for (int l = 0; l < 2; l++)
                    {
                        places2.obj[l] = l;
                    }

                    do
                    {
                        for (int l = 0; l < 4; l++)
                        {
                            pnr2.obj[l] = l;
                        }

                        do
                        {
                            for (int i = 1, k = 0; i < 7; i++)
                            {
                                if (places1.alphabet[places1.obj[0]] == Convert.ToChar(i) + 48 || places1.alphabet[places1.obj[1]] == Convert.ToChar(i) + 48)
                                {
                                    sw2.Write(a);
                                }
                                else if (places2.alphabet[places2.obj[0]] == Convert.ToChar(i) + 48 || places2.alphabet[places2.obj[1]] == Convert.ToChar(i) + 48)
                                {
                                    sw2.Write(b);
                                }
                                else
                                {
                                    sw2.Write(pnr2.alphabet[pnr2.obj[k]]);
                                    k++;
                                }

                            }
                            sw2.WriteLine();
                        } while (pnr2.NextPNR());
                    } while (places2.NextComb(2));
                } while (places1.NextComb(2));
            } while (choose_letter.NextComb(2));
            sw2.Close();
        }
    }
}
