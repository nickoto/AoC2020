using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day2
{
    class Program
    {
        static void Main(string[] args)
        {
            var data = File.ReadAllLines("input.txt");

            var regex = new Regex(@"^(\d*)-(\d*) (\w): (\S*)$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var totalValidPartI = 0;
            var totalValidPartII = 0;

            foreach (var item in data)
            {
                var policy = regex.Match(item);
                var min = int.Parse(policy.Groups[1].Value);
                var max = int.Parse(policy.Groups[2].Value);
                var letter = policy.Groups[3].Value;
                var pass = policy.Groups[4].Value;

                var found = new Regex(letter).Matches(pass).Count;

                if (found >= min && found <= max)
                {
                    totalValidPartI++;
                }

                if ((pass[min - 1] == letter[0]) ^ (pass[max - 1] == letter[0]))
                {
                    totalValidPartII++;
                }                
            }

            Console.WriteLine($"Total Valid Passwords (Part I): {totalValidPartI}");
            Console.WriteLine($"Total Valid Passwords (Part II): {totalValidPartII}");

        }
    }
}
