using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day07
{
    class Program
    {

        static void Main(string[] args)
        {
            var data = File.ReadAllLines("input.txt");
            var bagRegex = new Regex(@"^(.*) bags contain (.*)$", RegexOptions.Compiled);
            var containsRegex = new Regex(@"^ *(\d*) (.*) bag.*$", RegexOptions.Compiled);

            List<Tuple<int, string>> Splitty(string baglist)
            {
                if (baglist.StartsWith("no other bags")) return new List<Tuple<int, string>>();

                return baglist.Split(",").Select(x => containsRegex.Match(x)).Select(x => new Tuple<int, string>(
                    int.Parse(x.Groups[1].Value),
                    x.Groups[2].Value
                )).ToList();
            }

            var bagInfos = data.Select(x => bagRegex.Match(x)).Select(x => new {
                Color = x.Groups[1].Value,
                InnerBags = Splitty(x.Groups[2].Value)
            }).ToDictionary(key => key.Color, value => value.InnerBags);

            var allTerms = new List<string>{"shiny gold"};
            var searchTerms = allTerms.ToHashSet();

            do
            {
                var newBags = bagInfos.Where(x => x.Value.Any(y => searchTerms.Contains(y.Item2))).Select(x => x.Key).ToList();
                searchTerms = newBags.ToHashSet();

                allTerms.AddRange(newBags);
            }
            while(searchTerms.Any());

            Console.WriteLine($"Part I - {allTerms.Distinct().Count() - 1}");

            int BagCounty(string color)
            {   
                var bag = bagInfos[color];
                var total = 1;

                foreach(var innerBag in bag)
                {
                    var inner = BagCounty(innerBag.Item2);
                    total += innerBag.Item1 * inner;
                }

                return total;
            }

            Console.WriteLine($"Part II - {BagCounty("shiny gold") - 1}");
        }
    }
}
