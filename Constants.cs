namespace BirthdayExercise;

class Constants
{
    public const String _InputFileName = "id.txt";
    public const String _OutputFileName = "2010Analysis.txt";

    /// <summary>
    /// Returns the file path of data directory in project
    /// </summary>
    /// <returns>string</returns>
    public static string GetDataPath()
    {
        String environment = Environment.CurrentDirectory;
        return Directory.GetParent(environment).Parent.FullName.Replace(@"\bin", @"\Data\");

    }
}