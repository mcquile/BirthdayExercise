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
                if (IdentityNumberUtility.IsIdThirteenDigits(id))
                {
                    DateTime birthDate = DateTimeUtility.GetDateFromId(id);

                    if (BirthdayUtility.IsBirthdayInThePast(birthDate))
                    {
                        BirthdayUtility.IsBirthdayAfter2010(birthDate, ref before2010, ref after2010);
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
        /// Converts the DateTime argument to the desired string format and returns the resultant string.
        /// </summary>
        /// <param name="birthday">DateTime object</param>
        /// <returns>String</returns>
        private static String GetBirthdayFormat(DateTime birthday)
        {
            return birthday.ToString("dd/MM/yyyy");
        }
    }
}