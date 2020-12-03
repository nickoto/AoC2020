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
            var data = File.ReadAllLines("input.txt")
                .Select(x => int.Parse(x))
                .Where(x => x <= 2020)
                .OrderBy(x => x)
                .ToArray();

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

            do
            {
                var total = data[l] + data[m] + data[r];

                if (total == 2020)
                {
                    Console.WriteLine();
                    Console.WriteLine(data[l] * data[r] * data[m]);
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

                if (l == m)
                {
                    l = 0;
                    m++;
                    r = data.Length - 1;
                }

                Console.Write($"\r {l,4} {m,4} {r,4}");
            } while (true);
            


        }
    }
}