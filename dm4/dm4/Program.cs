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

namespace dm4
{
    class Program
    {
        static void Main(string[] args)
        {
            //4.1 ответ 75600 A(6,2)*C(7,2)*C(5,3)*A(4,2)
            PermNoRep pnr2 = new PermNoRep(6, 2);
            PermNoRep choose_letter = new PermNoRep(6, 2);
            Combinations places1 = new Combinations(7, 2);
            Combinations places2 = new Combinations(7, 3);
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
            places1.alphabet[6] = '7';
            StreamWriter sw2 = new StreamWriter(@"C:\Users\Asus\Documents\GitHub\dm3semester\dm4\dm4\task1.txt");
            for (int i = 0; i < 6; i++)
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
                places1.alphabet[6] = '7';

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
                    Array.Resize(ref places2.alphabet, 7);
                    places2.alphabet[0] = '1';
                    places2.alphabet[1] = '2';
                    places2.alphabet[2] = '3';
                    places2.alphabet[3] = '4';
                    places2.alphabet[4] = '5';
                    places2.alphabet[5] = '6';
                    places2.alphabet[6] = '7';

                    for (int l = 0; l < 7; l++)
                    {
                        if (places2.alphabet[l] == places1.alphabet[places1.obj[0]] || places2.alphabet[l] == places1.alphabet[places1.obj[1]])
                        {
                            places2.alphabet[l] = '0';
                        }
                    }
                    places2.n = 5;
                    Array.Sort(places2.alphabet);
                    Array.Reverse(places2.alphabet);
                    Array.Resize(ref places2.alphabet, 5);
                    Array.Reverse(places2.alphabet);
                    for (int l = 0; l < 3; l++)
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
                            for (int i = 1, k = 0; i < 8; i++)
                            {
                                if (places1.alphabet[places1.obj[0]] == Convert.ToChar(i) + 48 || places1.alphabet[places1.obj[1]] == Convert.ToChar(i) + 48)
                                {
                                    sw2.Write(a);
                                }
                                else if (places2.alphabet[places2.obj[0]] == Convert.ToChar(i) + 48 || places2.alphabet[places2.obj[1]] == Convert.ToChar(i) + 48 || places2.alphabet[places2.obj[2]] == Convert.ToChar(i) + 48)
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
                    } while (places2.NextComb(3));
                } while (places1.NextComb(2));
            } while (choose_letter.NextPNR());
            sw2.Close();

            //4.2 
            PermNoRep left = new PermNoRep(6, 2);
            Combinations choose_letter3 = new Combinations(6, 2);
            Combinations choose_letter4 = new Combinations(4, 1);
            Combinations places3 = new Combinations(9, 2);
            Combinations places4 = new Combinations(7, 2);
            Combinations places5 = new Combinations(5, 2);
            choose_letter3.alphabet[0] = 'a';
            choose_letter3.alphabet[1] = 'b';
            choose_letter3.alphabet[2] = 'c';
            choose_letter3.alphabet[3] = 'd';
            choose_letter3.alphabet[4] = 'e';
            choose_letter3.alphabet[5] = 'f';

            choose_letter4.alphabet[0] = 'a';
            choose_letter4.alphabet[1] = 'b';
            choose_letter4.alphabet[2] = 'c';
            choose_letter4.alphabet[3] = 'd';
            choose_letter4.alphabet[4] = 'e';
            choose_letter4.alphabet[5] = 'f';

            places3.alphabet[0] = '1';
            places3.alphabet[1] = '2';
            places3.alphabet[2] = '3';
            places3.alphabet[3] = '4';
            places3.alphabet[4] = '5';
            places3.alphabet[5] = '6';
            places3.alphabet[6] = '7';
            places3.alphabet[7] = '8';
            places3.alphabet[8] = '9';

            StreamWriter sw = new StreamWriter(@"C:\Users\Asus\Documents\GitHub\dm3semester\dm4\dm4\task2.txt");
            for (int i = 0; i < 2; i++)
            {
                choose_letter3.obj[i] = i;
            }
            for (int i = 0; i < 1; i++)
            {
                choose_letter4.obj[i] = i;
            }
            do
            {
                char a = choose_letter3.alphabet[choose_letter3.obj[0]];
                char b = choose_letter3.alphabet[choose_letter3.obj[1]];
                char c = choose_letter4.alphabet[choose_letter4.obj[0]];

                places3.alphabet[0] = '1';
                places3.alphabet[1] = '2';
                places3.alphabet[2] = '3';
                places3.alphabet[3] = '4';
                places3.alphabet[4] = '5';
                places3.alphabet[5] = '6';
                places3.alphabet[6] = '7';
                places3.alphabet[7] = '8';
                places3.alphabet[8] = '9';

                for (int j = 0; j < 2; j++)
                {
                    places3.obj[j] = j;
                }

                Array.Resize(ref left.alphabet, 6);
                left.alphabet[0] = 'a';
                left.alphabet[1] = 'b';
                left.alphabet[2] = 'c';
                left.alphabet[3] = 'd';
                left.alphabet[4] = 'e';
                left.alphabet[5] = 'f';

                for (int j = 0; j < 6; j++)
                {
                    if (left.alphabet[j] == a || left.alphabet[j] == b || left.alphabet[j]==c)
                    {
                        left.alphabet[j] = '0';
                    }
                }

                left.n = 3;
                Array.Sort(left.alphabet);
                Array.Reverse(left.alphabet);
                Array.Resize(ref left.alphabet, 3);
                Array.Reverse(left.alphabet);

                do
                {
                    Array.Resize(ref places4.alphabet, 7);
                    places2.alphabet[0] = '1';
                    places2.alphabet[1] = '2';
                    places2.alphabet[2] = '3';
                    places2.alphabet[3] = '4';
                    places2.alphabet[4] = '5';
                    places2.alphabet[5] = '6';
                    places2.alphabet[6] = '7';

                    for (int l = 0; l < 9; l++)
                    {
                        if (places2.alphabet[l] == places1.alphabet[places1.obj[0]] || places2.alphabet[l] == places1.alphabet[places1.obj[1]])
                        {
                            places2.alphabet[l] = '0';
                        }
                    }
                    places2.n = 7;
                    Array.Sort(places2.alphabet);
                    Array.Reverse(places2.alphabet);
                    Array.Resize(ref places2.alphabet, 7);
                    Array.Reverse(places2.alphabet);
                    for (int l = 0; l < 2; l++)
                    {
                        places2.obj[l] = l;
                    }

                    do
                    {
                        for (int l = 0; l < 4; l++)
                        {
                            left.obj[l] = l;
                        }

                        do
                        {
                            for (int i = 1, k = 0; i < 8; i++)
                            {
                                if (places3.alphabet[places3.obj[0]] == Convert.ToChar(i) + 48 || places3.alphabet[places3.obj[1]] == Convert.ToChar(i) + 48)
                                {
                                    sw.Write(a);
                                }
                                else if (places4.alphabet[places4.obj[0]] == Convert.ToChar(i) + 48 || places4.alphabet[places4.obj[1]] == Convert.ToChar(i) + 48 || places4.alphabet[places3.obj[2]] == Convert.ToChar(i) + 48)
                                {
                                    sw.Write(b);
                                }
                                else if (places5.alphabet[places5.obj[0]] == Convert.ToChar(i) + 48 || places5.alphabet[places5.obj[1]] == Convert.ToChar(i) + 48 || places5.alphabet[places5.obj[2]] == Convert.ToChar(i) + 48)
                                {
                                    sw.Write(c);
                                }
                                else
                                {
                                    sw.Write(left.alphabet[left.obj[k]]);
                                    k++;
                                }

                            }
                            sw.WriteLine();
                        } while (pnr2.NextPNR());
                    } while (places4.NextComb(3));
                } while (places3.NextComb(2));
            } while (choose_letter3.NextComb(2));
            sw.Close();
        }

    }
}
