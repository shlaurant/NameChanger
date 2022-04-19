﻿using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using CsvHelper;

namespace NameChanger
{
    class Program
    {
        private static string dataFileName = "names.csv";

        static void Main(string[] args)
        {
            var koreanNames = FileNamesIn(args[0]);
            var englishNames = FileNamesIn(args[1]);

            koreanNames = TrimExtensionOf(koreanNames);
            englishNames = TrimExtensionOf(englishNames);

            var names = new List<Name>();
            for (var index = 0; index < koreanNames.Count; index++)
            {
                names.Add(new Name
                {
                    Korean = koreanNames[index],
                    English = englishNames[index]
                });
            }

            WriteData(names, args[2]);
        }

        private static void WriteData(List<Name> names, string outputPath)
        {
            using (var writer =
                new StreamWriter(
                    File.OpenWrite(outputPath + "\\" + dataFileName),
                    Encoding.UTF8))
            {
                using (var csv =
                    new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.WriteRecords(names);
                }
            }
        }

        private static List<string> TrimExtensionOf(List<string> fileNames)
        {
            return fileNames.Select(name => name.Split('.')[0]).ToList();
        }

        private static List<string> FileNamesIn(string directoryPath)
        {
            return Directory.GetFiles(directoryPath)
                .Select(file => file.Split('\\').Last()).ToList();
        }
    }

    public class Name
    {
        public string Korean { get; set; }
        public string English { get; set; }
    }
}