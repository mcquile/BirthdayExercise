using System.Globalization;
using System.Text.RegularExpressions;


namespace BirthdayExercise
{
    /// <summary>
    /// Class to serve as an entry point and contain various utility methods.
    /// </summary>
    class Program
    {

        /// <summary>
        /// Entry point of application. Contains the main loop and flow of program.
        /// Loops through ID numbers obtained by reading file, prints birthday derived from ID number and tracks birthdays occuring before and after 01/01/2010.
        /// Finally writes the 2010 analysis to a file stored in the data directory.
        /// </summary>
        /// <param name="args">Command-Line arguments</param>
        static void Main(String[] args)
        {
            int before2010 = 0;
            int after2010 = 0;

            String[] idArray = InputOutput.ReadFileAndReturnArray("id.txt"); 

            foreach (String id in idArray)
            {
                if (IsIdValid(id))
                {
                    DateTime birthDate = GetDateFromId(id);

                    if (IsBirthdayValid(birthDate))
                    {
                        IsBirthdayAfter2010(birthDate, ref before2010, ref after2010);
                        Console.WriteLine(GetBirthdayFormat(birthDate));
                        continue;
                    }
                }

                String errorMessage = String.IsNullOrWhiteSpace(id) ? "EMPTY ENTRY" : id;
                Console.WriteLine($"Error: {errorMessage}");
            }

            InputOutput.WriteToFile(before2010, after2010);
        }

        /// <summary>
        /// Reads file and returns a string array with each element being a line from the file.
        /// </summary>
        /// <param name="fileName">File(path) name containing data to read</param>
        /// <returns>(string[]) String array of each line in file</returns>
        public static string[] ReadFileAndReturnArray(string fileName)
        {
            return InputOutput.ReadFileAndReturnArray(fileName);
        }

        /// <summary>
        /// Returns the file path of data directory in project
        /// </summary>
        /// <returns>string</returns>
        public static string GetDataPath()
        {
            String environment = Environment.CurrentDirectory;
            return Directory.GetParent(environment).Parent.FullName.Replace(@"\bin", @"\Data\");

        }

        /// <summary>
        /// Checks if a id(string) conforms to having a length of 13 characters with all being digits ranging from 0 to 9.
        /// </summary>
        /// <param name="id">String of id number to validate</param>
        /// <returns>Boolean</returns>
        private static Boolean IsIdValid(string id)
        {
            return Regex.IsMatch(id, "^\\d{13}$");
        }

        /// <summary>
        /// Uses the first 6 characters from the id(string) to generate and return a DateTime object. Will return null should a format exception be thrown during parsing.
        /// </summary>
        /// <param name="id">String of id number</param>
        /// <returns>DateTime or null</returns>
        private static DateTime GetDateFromId(string id)
        {
            String birthdaySequence = id.Substring(0, 6);
            try
            {
                return DateTime.ParseExact(birthdaySequence, "yyMMdd", CultureInfo.InvariantCulture);
            }
            catch (FormatException)
            {
                return DateTime.Now.AddYears(1);
            }
        }

        /// <summary>
        /// Checks if nullable DateTime argument passed is before the current date and is not null.
        /// </summary>
        /// <param name="birthday">DateTime nullable of birthday</param>
        /// <returns>Boolean</returns>
        private static Boolean IsBirthdayValid(DateTime birthday)
        {
            return DateTime.Now.CompareTo(birthday) > 0;
        }

        /// <summary>
        /// Converts the DateTime argument to the desired string format and returns the resultant string.
        /// </summary>
        /// <param name="birthday">DateTime object</param>
        /// <returns>String</returns>
        private static String GetBirthdayFormat(DateTime birthday)
        {
            return birthday.ToString("dd/MM/yyyy");
        }

        /// <summary>
        /// Checks if the DateTime argument passed is before or after 01/01/2010. Updates the relevent integer and returns the values.
        /// </summary>
        /// <param name="birthday">DateTime object containing date to compare</param>
        /// <param name="before2010">int used to track how many dates are before 01/01/2010</param>
        /// <param name="after2010">int used to track how many dates are after 01/01/2010</param>
        /// <returns></returns>
        private static void IsBirthdayAfter2010(DateTime birthday, ref int before2010, ref int after2010)
        {
            DateTime newYears2010 = new DateTime(2010, 01, 01);
            (before2010, after2010) = birthday.CompareTo(newYears2010) > 0 ?  (before2010, after2010+1) : (before2010+1, after2010);
        }
    }
}