namespace BirthdayExercise;

class InputOutput
{
    /// <summary>
    /// Reads file and returns a string array with each element being a line from the file.
    /// </summary>
    /// <param name="fileName">File(path) name containing data to read</param>
    /// <returns>(string[]) String array of each line in file</returns>
    public static string[] ReadFileAndReturnArray(string fileName)
    {
        string filePath = Constants.GetDataPath() + fileName;

        try
        {
            return File.ReadAllLines(filePath);
        }
        catch (Exception)
        {
            return Array.Empty<string>();
        }
    }

    /// <summary>
    /// Receives two integer arguments tracking birthdays before and after 01/01/2010 and writes the information to a file called "2010Analysis.txt" in the Data directory.
    /// </summary>
    /// <param name="before2010">int used to track how many dates are before 01/01/2010</param>
    /// <param name="after2010">int used to track how many dates are after 01/01/2010</param>
    public static void WriteToFile(int before2010, int after2010)
    {
        String fileName = Constants.GetDataPath() + "2010Analysis.txt";

        string[] lines =
        {
            $"Number of people born before 01/01/2010: {before2010}",
            $"Number of people born after 01/01/2010: {after2010}"
        };

        File.WriteAllLines(fileName, lines);
    }
}