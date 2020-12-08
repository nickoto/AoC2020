using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day04
{
    class Program
    {
        /**
         * byr (Birth Year)
         * iyr (Issue Year)
         * eyr (Expiration Year)
         * hgt (Height)
         * hcl (Hair Color)
         * ecl (Eye Color)
         * pid (Passport ID)
         * cid (Country ID)
         */

        class Passport
        {
            public string byr { get; set; }
            public string iyr { get; set; }
            public string eyr { get; set; }
            public string hgt { get; set; }
            public string hcl { get; set; }
            public string ecl { get; set; }
            public string pid { get; set; }
            public string cid { get; set; }

            static Regex hgtRegex = new Regex(@"^(\d*)(in|cm)$", RegexOptions.Compiled);
            static Regex hclRegex = new Regex(@"^#([0-9a-f]{6})$", RegexOptions.Compiled);
            static Regex eclRegex = new Regex(@"^(amb|blu|brn|gry|grn|hzl|oth)$", RegexOptions.Compiled);
            static Regex pidRegex = new Regex(@"^(\d{9})$", RegexOptions.Compiled);


            public bool IsValid
            {
                get
                {
                    return byr != null && iyr != null && eyr != null & hgt != null
                            && hcl != null && ecl != null && pid != null;
                }
            }

            public bool IsStrictValid
            {
                get
                {
                    if (!this.IsValid) return false;

                    try
                    {
                        bool isValid = true;

                        int byr = int.Parse(this.byr);
                        isValid &= byr >= 1920 && byr <= 2002;

                        int iyr = int.Parse(this.iyr);
                        isValid &= iyr >= 2010 && iyr <= 2020;

                        int eyr = int.Parse(this.eyr);
                        isValid &= eyr >= 2020 && eyr <= 2030;

                        var hgt = hgtRegex.Match(this.hgt);
                        isValid &= hgt.Success;
                        if (isValid)
                        {
                            var height = int.Parse(hgt.Groups[1].Value);
                            var unit = hgt.Groups[2].Value;

                            if (unit == "cm")
                            {
                                isValid &= height >= 150 && height <= 193;
                            }
                            else
                            {
                                isValid &= height >= 59 && height <= 76;
                            }
                        }

                        isValid &= hclRegex.Match(this.hcl).Success;
                        isValid &= eclRegex.Match(this.ecl).Success;
                        isValid &= pidRegex.Match(this.pid).Success;

                        return isValid;
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                }
            }

            public static string GetOrNull(IDictionary<string, string> d, string key)
            {
                if (d.TryGetValue(key, out var v))
                {
                    return v;
                }

                return null;
            }

            public static Passport Parse(string passportData)
            {
                var fields = passportData.Split(" ")
                    .Select(x => x.Split(":"))
                    .Where(x => x.Length == 2)
                    .ToDictionary(k => k[0], v => v[1]);

                return new Passport
                {
                    byr = GetOrNull(fields, "byr"),
                    iyr = GetOrNull(fields, "iyr"),
                    eyr = GetOrNull(fields, "eyr"),
                    hgt = GetOrNull(fields, "hgt"),
                    hcl = GetOrNull(fields, "hcl"),
                    ecl = GetOrNull(fields, "ecl"),
                    pid = GetOrNull(fields, "pid"),
                    cid = GetOrNull(fields, "cid"),
                };
            }
        }

        static IEnumerable<string> PassportData(string[] lines)
        {
            var accumulator = new List<string>();
            foreach(var line in lines)
            {
                if (line == "")
                {
                    yield return string.Join(" ", accumulator);
                    accumulator.Clear();
                }
                else
                {
                    accumulator.Add(line);
                }
            }

            if (accumulator.Count > 0)
            {
                yield return string.Join(" ", accumulator);
            }
        }

        static void Main(string[] args)
        {
            var data = File.ReadAllLines("input.txt");
            var passports = PassportData(data).Select(x => Passport.Parse(x)).ToList();

            Console.WriteLine("Part I");
            Console.WriteLine($"Valid Passports: {passports.Where(x => x.IsValid).Count()}");

            Console.WriteLine("Part II");
            Console.WriteLine($"Valid Passports: {passports.Where(x => x.IsStrictValid).Count()}");
        }
    }
}
