using System.Text.RegularExpressions;

namespace BirthdayExercise;

class IdentityNumberUtility
{
    /// <summary>
    /// Checks if a id(string) conforms to having a length of 13 characters with all being digits ranging from 0 to 9.
    /// </summary>
    /// <param name="id">String of id number to validate</param>
    /// <returns>Boolean</returns>
    public static Boolean IsIdThirteenDigits(string id)
    {
        return Regex.IsMatch(id, "^\\d{13}$");
    }
}