using System;
using System.Collections.Generic;
using System.Text;

namespace SharpOS
{
    class Tests
    {
        public static int DeadCodeElimination(int b, int c)
        {
            int a, d, e, f;

            a = b + c; // Useless Code
            d = b - c; // Useless Code
            e = 100;
            e = b * c + e;
            f = b / c; // Useless Code

            return e;
        }

        public static int Test(int a)
        {
            if (a < 5)
            {
                a = a * 5;
            }
            else
            {
                a = a + 5;
            }

            if (a == 10)
            {
                a = 100;
            }

            return a;
        }

        public static int Dominance(int b, int c, int d, int f)
        {
            int e, p, r, y, z, q, s, u, x;
            int a = 5;
            int n = a + b;

            if (n < 10)
            {
                do
                {
                    q = a + b;
                    r = c + d;

                    if (q > r)
                    {
                        e = b + 18;
                        s = a + b;
                        u = e + f;
                    }
                    else
                    {
                        e = a + 17;
                        u = e + f;
                    }

                    x = e + f;

                    Console.WriteLine(x);
                }
                while (x < 1);
            }
            else
            {
                p = c + d;
                r = c + d;
            }

            y = a + b;
            z = r + d;

            return z;
        }

        public static int fib(int n)
        {
            if (n < 2)
                return 1;
            return fib(n - 2) + fib(n - 1);
        }

        public static int test_0_many_nested_loops()
        {
            // we do the loop a few times otherwise it's too fast
            for (int i = 0; i < 5; ++i)
            {
                int n = 16;
                int x = 0;
                int a = n;
                while (a-- != 0)
                {
                    int b = n;
                    while (b-- != 0)
                    {
                        int c = n;
                        while (c-- != 0)
                        {
                            int d = n;
                            while (d-- != 0)
                            {
                                int e = n;
                                while (e-- != 0)
                                {
                                    int f = n;
                                    while (f-- != 0)
                                    {
                                        x++;
                                    }
                                }
                            }
                        }
                    }
                }
                if (x != 16777216)
                    return 1;
            }
            return 0;
        }

        public static int test_0_logic_run()
        {
            // GPL: Copyright (C) 2001  Southern Storm Software, Pty Ltd.
            int iter, i = 0;

            while (i++ < 10)
            {
                // Initialize.
                bool flag1 = true;
                bool flag2 = true;
                bool flag3 = true;
                bool flag4 = true;
                bool flag5 = true;
                bool flag6 = true;
                bool flag7 = true;
                bool flag8 = true;
                bool flag9 = true;
                bool flag10 = true;
                bool flag11 = true;
                bool flag12 = true;
                bool flag13 = true;

                // First set of tests.
                for (iter = 0; iter < 2000000; ++iter)
                {
                    if ((flag1 || flag2) && (flag3 || flag4) &&
                       (flag5 || flag6 || flag7))
                    {
                        flag8 = !flag8;
                        flag9 = !flag9;
                        flag10 = !flag10;
                        flag11 = !flag11;
                        flag12 = !flag12;
                        flag13 = !flag13;
                        flag1 = !flag1;
                        flag2 = !flag2;
                        flag3 = !flag3;
                        flag4 = !flag4;
                        flag5 = !flag5;
                        flag6 = !flag6;
                        flag1 = !flag1;
                        flag2 = !flag2;
                        flag3 = !flag3;
                        flag4 = !flag4;
                        flag5 = !flag5;
                        flag6 = !flag6;
                    }
                }
            }
            return 0;
        }
        
        public static int test_3524578_fib()
        {
            for (int i = 0; i < 10; i++)
                fib(32);

            return fib(32);
        }

        private static ulong numMoves;

        static void movetower(int disc, int from, int to, int use)
        {
            if (disc > 0)
            {
                numMoves++;
                movetower(disc - 1, from, use, to);
                movetower(disc - 1, use, to, from);
            }
        }

        public static int test_0_hanoi()
        {
            int iterations = 5000;
            int numdiscs = 12;

            numMoves = 0;
            while (iterations > 0)
            {
                iterations--;
                movetower(numdiscs, 1, 3, 2);
            }
            if (numMoves != 20475000)
                return 1;
            return 0;
        }

        /*public static int test_0_castclass()
        {
            object a = "a";

            for (int i = 0; i < 100000000; i++)
            {
                string b = (string)a;
                if ((object)a != (object)b)
                    return 1;
            }
            return 0;
        }*/

        public static int test_23005000_float()
        {
            double a, b, c, d;
            bool val;
            int loops = 0;
            a = 0.0;
            b = 0.0001;
            c = 2300.5;
            d = 1000.0;

            while (a < c)
            {
                if (a == d)
                    b *= 2;
                a += b;
                val = b >= c;
                if (val) break;
                loops++;
            }
            return loops;
        }

        /*public static int test_1028_sieve()
        {
            int NUM = 2000;
            byte[] flags = new byte[8192 + 1];
            int i, k;
            int count = 0;

            while (NUM-- != 0)
            {
                count = 0;
                for (i = 2; i <= 8192; i++)
                {
                    flags[i] = 1;
                }
                for (i = 2; i <= 8192; i++)
                {
                    if (flags[i] != 0)
                    {
                        // remove all multiples of prime: i
                        for (k = i + i; k <= 8192; k += i)
                        {
                            flags[k] = 0;
                        }
                        count++;
                    }
                }
            }
            //printf("Count: %d\n", count);
            return (count);
        }*/
    }
}
