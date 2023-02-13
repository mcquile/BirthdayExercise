using System.Text;
using System.Text.RegularExpressions;

namespace BirthdayExercise;

class IdentityNumberUtility
{
    internal static Boolean IsIdValid(string id)
    {
        return IsIdStructureCorrect(id) && IsControlDigitValid(id);
    }

    /// <summary>
    /// Checks if a id(string) conforms to having a length of 13 characters with all being digits ranging from 0 to 9.
    /// </summary>
    /// <param name="id">String of id number to validate</param>
    /// <returns>Boolean</returns>
    internal static Boolean IsIdStructureCorrect(String id)
    {
        return Regex.IsMatch(id, @"^\d{10}(0|1)\d{2}$");
    }

    /// <summary>
    /// Checks if last digit of ID number is correct according to Luhn's algorithm
    /// </summary>
    /// <param name="id">String of id number to validate</param>
    /// <returns>Boolean</returns>
    private static Boolean IsControlDigitValid(String id)
    {
        int evenTotal = GetEvenIndexTotal(id);
        int oddTotal = GetOddIndexTotal(id);
        if (evenTotal>0 && oddTotal>0)
        {
            String expectedControlDigit = GetExpectedControlDigit(evenTotal, oddTotal);
            String actualControlDigit = id[12].ToString();
            return actualControlDigit.Equals(expectedControlDigit);
        }
        return false;
    }

    /// <summary>
    /// Adds all the digits at even indices in ID number (0,2,4...12) and returns the sum
    /// </summary>
    /// <param name="id">String of id number to validate</param>
    /// <returns>int</returns>
    private static int GetEvenIndexTotal(String id)
    {
        StringBuilder stringBuilder = new StringBuilder();
        for (int i = 0; i < id.Length-1; i+=2)
        {
            stringBuilder.Append(id[i]);
        }

        return AddDigitsInString(stringBuilder.ToString());
    }


    /// <summary>
    /// Creates a new number from all the digits at odd indices in ID number (1,3,5...11), multiplies the resultant number by two then returns the sum of the digits.
    /// </summary>
    /// <param name="id">String of id number to validate</param>
    /// <returns>int</returns>
    private static int GetOddIndexTotal(String id)
    {
        StringBuilder stringBuilder = new StringBuilder();
        for (int i = 1; i < id.Length-1; i+=2)
        {
            stringBuilder.Append(id[i]);
        }

        bool parseSuccessful = int.TryParse(stringBuilder.ToString(), out int oddNumberSequence);

        if (parseSuccessful)
        {
            return AddDigitsInString((oddNumberSequence * 2).ToString());
        }

        return -1;
    }

    /// <summary>
    /// Adds the digits of a provided string and returns the total. Will return -1 if provided string is invalid.
    /// </summary>
    /// <param name="strNumber">String containing only digits</param>
    /// <returns>int</returns>
    private static int AddDigitsInString(String strNumber)
    {
        int total = 0;
        foreach (char character in strNumber)
        {
            bool parseSuccessful = int.TryParse(character.ToString(), out int digit);
            if (parseSuccessful)
            {
                total += digit;
            }
            else
            {
                return -1;
            }
        }

        return total;
    }

    /// <summary>
    /// Gets the sum of two integers provided and returns 10 - {last digit of the sum}
    /// </summary>
    /// <param name="evenTotal">int</param>
    /// <param name="oddTotal">int</param>
    /// <returns>String</returns>
    private static String GetExpectedControlDigit(int evenTotal, int oddTotal)
    {
        int lastDigitOfTotal = (evenTotal + oddTotal) % 10;
        return (10 - lastDigitOfTotal).ToString();
    }
}