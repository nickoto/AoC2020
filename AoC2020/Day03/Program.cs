using System;
using System.IO;
using System.Linq;

namespace Day03
{
    class Program
    {
        static long CountTrees(string[] data, int slopex, int slopey)
        {
            var w = data[0].Length;
            var h = data.Length;

            var x = 0;
            var y = 0;
            var trees = 0;

            while (y < h)
            {
                if (data[y][x % w] == '#') trees++;

                x += slopex;
                y += slopey;
            }

            return trees;
        }

        static void PartI(string[] data)
        {
            Console.WriteLine($"Part I - Tree count: {CountTrees(data, 3, 1)}");
        }

        static void PartII(string[] data)
        {
            var trees = new[]
            {
                CountTrees(data, 1, 1),
                CountTrees(data, 3, 1),
                CountTrees(data, 5, 1),
                CountTrees(data, 7, 1),
                CountTrees(data, 1, 2)
            };

            Console.WriteLine($"Part II - Tree count: { trees.Aggregate(1L, (cur, next) => cur * next)}");
        }

        static void Main(string[] args)
        {
            var data = File.ReadAllLines("input.txt");

            PartI(data);
            PartII(data);
        }
    }
}
