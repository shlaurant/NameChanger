using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;

namespace NameChanger
{
    class Program
    {
        static void Main(string[] args)
        {
            var files = Directory.GetFiles(DirectoryPath(args));
            foreach (var file in files)
            {
                Console.WriteLine(file);
            }

            var test = new List<Name>();
            test.Add(new Name{Korean = "ss"});
            new CsvWriter(new StreamWriter(DirectoryPath(args) + "\\names.csv"),
                CultureInfo.InvariantCulture).WriteRecords(test);
        }

        private static List<Name> FileNames(string[] files)
        {
            return files.ToList()
                .ConvertAll(file => new Name {Korean = file});
        }

        private static string DirectoryPath(string[] args)
        {
            return args[0];
        }
    }

    public class Name
    {
        public string Korean { get; set; }
    }
}