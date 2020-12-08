using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day08
{
    class Program
    {        
        enum Instruction
        {
            NOP,
            ACC,
            JMP
        }

        class Op
        {
            public Instruction op {get; set;}
            public int operand {get;set;}
            public int count {get; set;}
        }
        
        static void Main(string[] args)
        {
            var data = File.ReadAllLines("input.txt")
                .Select(x => x.Split(" "))
                .Select(x => new Op {
                op = (Instruction) Enum.Parse(typeof(Instruction), x[0].ToUpperInvariant()),
                operand = int.Parse(x[1]),
                count = 0
            }).ToList();

            int ip = 0;
            int acc = 0;

            void Run() {
                while(ip < data.Count && data[ip].count == 0)
                {
                    var i = data[ip];
                    i.count += 1;

                    switch(i.op)
                    {
                        case Instruction.NOP:
                            ip++;
                            break;
                        case Instruction.ACC:
                            acc += i.operand;
                            ip += 1;
                            break;
                        case Instruction.JMP:
                            ip += i.operand;
                            break;
                        default:
                            Console.WriteLine("error");
                            break;
                    }
                }
            }

            Run();
            Console.WriteLine($"Part I - {acc}");

            var changeable = data.Select((x, idx) => new {x, idx}).Where((x, idx) => x.x.op != Instruction.ACC && x.x.count > 0).Select(x => x.idx).ToList();

            ip = 0;
            acc = 0;

            while (ip < data.Count)
            {
                ip = 0;
                acc = 0;

                var swappedIndex = changeable[0];
                changeable.RemoveAt(0);

                var original = data[swappedIndex].op;

                // Swap
                if (original == Instruction.NOP) {
                    data[swappedIndex].op = Instruction.ACC;
                } else {
                    data[swappedIndex].op = Instruction.NOP;
                }

                // Reset.
                foreach(var x in data) { 
                    x.count = 0;
                }

                Run();

                // Swap back
                data[swappedIndex].op = original;

                Console.WriteLine($"Swapped Index: {swappedIndex}  IP={ip} ACC={acc}");
            }
        }
    }
}
