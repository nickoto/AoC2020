using System;
using System.IO;
using System.Linq;

namespace Day1
{
    class Program
    {
        static void Main(string[] args)
        {
            // PART I - 2 Entries that sum to 2020.
            var datas = File.ReadAllLines("input.txt")
                .Select(x => int.Parse(x))
                .Where(x => x <= 2020)
                .OrderBy(x => x);

            var min = datas.First();
            
            // Can actually filter out even more.
            var data = datas.Where(x => x <= 2020 - min).ToArray();

            var l = 0;
            var r = data.Length - 1;

            do
            {
                var total = data[l] + data[r];

                if (total == 2020)
                {
                    Console.WriteLine(data[l] * data[r]);
                    break;
                }
                else if (total > 2020)
                {
                    r--;
                }
                else
                {
                    l++;
                }
            }
            while (l <= r);

            // PART II - 3 Entries that sum to 2020
            l = 0;
            r = data.Length - 1;
            var m = 1;
            var dir = -1;

            do
            {
                var total = data[l] + data[m] + data[r];
                Console.WriteLine($"{l,4} {m,4} {r,4} => {total,12}");

                if (total == 2020)
                {
                    Console.WriteLine();
                    Console.WriteLine(total);
                    Console.WriteLine(data[l] * data[r] * data[m]);
                    break;
                }

                if (dir < 0)
                {
                    if (total < 2020)
                    {
                        m++;
                    }
                    else
                    {
                        r--;
                    }

                    if (m == r)
                    {
                        l++;
                        dir = 1;
                    }
                }
                else
                {
                    if (total < 2020)
                    {
                        r++;
                    }
                    else
                    {
                        m--;
                    }

                    if (m == l)
                    {
                        l++;
                        m += 2;
                        dir = -1;
                    }
                }

            } while (r != l && l != m);
        }
    }
}