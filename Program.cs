using System;
using System.Globalization;
using System.Text.RegularExpressions;


namespace BirthdayExercise
{
    class birthdayEvaluator
    {
        static void Main(string[] args)
        {

            var before2010 = 0;
            var after2010 = 0;

            var idArray = readFileAndReturnArray(@"C:\Users\bbdnet2874\Documents\C#\BirthdayExercise\Data\id.txt");

            foreach (var id in idArray)
            {
                if (isIdValid(id))
                {
                    var birthDate = getDateFromID(id);

                    if (isBirthdayValid(birthDate))
                    {
                        (before2010, after2010) = isBirthdayAfter2010((DateTime)birthDate, before2010, after2010);
                        Console.WriteLine(getBirthdayFormat(birthDate.Value));
                        continue;
                    }
                }
                Console.WriteLine($"Error: {id}");
            }
            writeToFile(before2010, after2010);
        }

        protected static string[] readFileAndReturnArray(string fileName)
        {
            try
            {
                fileName = @"C:\Users\bbdnet2874\Documents\C#\BirthdayExercise\Data\id.txt";
                return File.ReadAllLines(fileName);
            }
            catch (System.Exception)
            {
                return Array.Empty<string>();
            }
        }

        private static Boolean isIdValid(string id)
        {
            return Regex.IsMatch(id, "^\\d{13}$");
        }

        private static DateTime? getDateFromID(string id)
        {
            var birthdaySequence = id.Substring(0, 6);
            try
            {
                return DateTime.ParseExact(birthdaySequence, "yyMMdd", CultureInfo.InvariantCulture);
            }
            catch (FormatException)
            {
                return null;
            }
        }

        private static Boolean isBirthdayValid(DateTime? birthday)
        {
            return birthday != null && DateTime.Now.CompareTo(birthday) > 0;
        }

        private static String getBirthdayFormat(DateTime birthday)
        {
            return birthday.ToString("dd/MM/yyyy");
        }

        private static (int, int) isBirthdayAfter2010(DateTime birthday, int before2010, int after2010)
        {
            var newYears2010 = new DateTime(2010, 01, 01);

            (before2010, after2010) = birthday.CompareTo(newYears2010) > 0 ?  (before2010, after2010+1) : (before2010+1, after2010);

            return (before2010, after2010);
        }

        private static void writeToFile(int before2010, int after2010)
        {
            string[] lines =
            {
                $"Number of people born before 01/01/2010: {before2010}",
                $"Number of people born after 01/01/2010: {after2010}"
            };

             File.WriteAllLines(@"C:\Users\bbdnet2874\Documents\C#\BirthdayExercise\Data\2010Analysis.txt", lines);
        }
    }
}