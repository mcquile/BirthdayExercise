using System.Globalization;

namespace BirthdayExercise;

class DateTimeUtility
{
    /// <summary>
    /// Uses the first 6 characters from the id(string) to generate and return a DateTime object. Will return null should a format exception be thrown during parsing.
    /// </summary>
    /// <param name="id">String of id number</param>
    /// <returns>DateTime or null</returns>
    public static DateTime GetDateFromId(string id)
    {
        String birthdaySequence = id.Substring(Constants._BirthdayStartIndex, Constants._BirthdayLength);
        try
        {
            return DateTime.ParseExact(birthdaySequence, "yyMMdd", CultureInfo.InvariantCulture);
        }
        catch (FormatException)
        {
            return DateTime.Now.AddYears(1);
        }
    }
}