/**
 *  (C) 2006-2007 Mircea-Cristian Racasan <darx_kies@gmx.net>
 * 
 *  Licensed under the terms of the GNU GPL License version 2.
 * 
 */

using System;
using System.Collections.Generic;
using System.Text;

namespace AOT
{
    public class Misc
    {
        public static UInt64 TypesU(int intValue, long longValue, UInt16 int16Value, UInt32 int32Value, UInt64 int64Value)
        {
            intValue -= 10;
            longValue = -longValue;
            int16Value *= 10;
            int32Value /= 10;
            int64Value += int64Value;

            return (UInt32)intValue + (UInt32)longValue + int16Value + int32Value + int64Value;
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

                    //Console.WriteLine(x);
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

        public static long Types(byte byteValue, int intValue, long longValue, Int16 int16Value, Int32 int32Value, Int64 int64Value, float floatValue, double doubleValue)
        {
            byteValue += 10;
            intValue -= 10;
            int16Value *= 10;
            int32Value /= 10;
            int64Value = -int64Value;
            longValue = longValue - 10;
            floatValue *= 2.0f + int32Value;
            doubleValue *= 4.0d + int64Value;

            return longValue + byteValue + intValue + int16Value + int32Value + int64Value + (long)floatValue + (long)doubleValue;
        }

        public static bool TypeBool(bool value)
        {
            return !value;
        }

        public static long TypeD2L(double value)
        {
            return (long)value;
        }

        public static double TstDouble(double a, double b)
        {
            a = b * a;
            b = a / 5;
            a = b + 5;

            return a * b;
        }

        public static float TstFloat(float a, float b)
        {
            return a * b + b / 5;
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

        public static void Tst()
        {
            long x = 0xDEADBEEFDEAD;

            x = TstLong(x);

            Console.WriteLine("Result: " + x.ToString());

            Console.WriteLine("Result: " + TstInt(0xDEAD, 0xBEEF).ToString());
        }

        public static long TstLong(long x)
        {
            long y = x * 100;

            x = y / 5 + x;

            return x * 4 + 5;
        }

        public static int TstInt(int x, int z)
        {
            int y = x * 100;

            x = y / 5 + x;

            return x * 4 + 5 - z;
        }
    }       
}