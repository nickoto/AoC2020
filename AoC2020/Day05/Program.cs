using System;
using System.IO;
using System.Linq;

namespace Day05
{
    class Program
    {
        struct BoardingPass
        {
            public int Row { get; init; }
            public int Col { get; init; }
            public int Seat { get; init; }

            public static BoardingPass Parse(string src)
            {
                var temp = src.Aggregate(0, (acc, c) => (acc << 1) + ((c == 'B' || c == 'R') ? 1 : 0));
                var row = temp >> 3;
                var col = temp & 7;

                return new BoardingPass
                {
                    Row = row,
                    Col = col,
                    Seat = row * 8 + col
                };
            }
        }


        static void Main(string[] args)
        {
            var data = File.ReadAllLines("input.txt");

            var passes = data.Select(x => BoardingPass.Parse(x)).OrderBy(x => x.Seat).ToList();

            Console.WriteLine($"Part I - Highest seat ID: {passes.Select(x => x.Seat).Max()}");

            var firstSeat = passes[0].Seat;
            var missingId = passes.Select((pass, index) => new { pass.Seat, index }).First(x => x.Seat != x.index + firstSeat);

            Console.WriteLine($"Part II - Your Seat: {missingId.Seat - 1}");
        }
    }
}
