using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day06
{
    class Program
    {
        static IEnumerable<IList<string>> SplitOnBlankLines(string[] lines)
        {
            var accumulator = new List<string>();
            foreach (var line in lines)
            {
                if (line == "")
                {
                    yield return accumulator;
                    accumulator = new List<string>();
                }
                else
                {
                    accumulator.Add(line);
                }
            }

            if (accumulator.Count > 0)
            {
                yield return accumulator;
            }
        }

        static void Main(string[] args)
        {
            var data = File.ReadAllLines("input.txt");
            var groups = SplitOnBlankLines(data).ToList();
            
            var groupCounts = groups.Select(x => string.Join("", x).Select(y => y).Distinct().Count());
            Console.WriteLine($"Part I - {groupCounts.Sum()}");

            var countAnsweredYesByAll = groups.Select(x => string.Join("", x).GroupBy(key => key, value => value).Where(y => y.Count() == x.Count).Count());
            Console.WriteLine($"Part II - {countAnsweredYesByAll.Sum()}");
        }
    }
}
