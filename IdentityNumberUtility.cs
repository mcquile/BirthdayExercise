using System.Text;
using System.Text.RegularExpressions;

namespace BirthdayExercise;

class IdentityNumberUtility
{
    public static Boolean IsIdValid(string id)
    {
        return IsIdStructureCorrect(id) && IsControlDigitValid(id);
    }

    /// <summary>
    /// Checks if a id(string) conforms to having a length of 13 characters with all being digits ranging from 0 to 9.
    /// </summary>
    /// <param name="id">String of id number to validate</param>
    /// <returns>Boolean</returns>
    public static Boolean IsIdStructureCorrect(String id)
    {
        return Regex.IsMatch(id, @"^\d{10}(0|1)\d{2}$");
    }

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

    private static int GetEvenIndexTotal(String id)
    {
        StringBuilder stringBuilder = new StringBuilder();
        for (int i = 0; i < id.Length-1; i+=2)
        {
            stringBuilder.Append(id[i]);
        }

        return AddDigitsInString(stringBuilder.ToString());
    }

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

    private static String GetExpectedControlDigit(int evenTotal, int oddTotal)
    {
        int lastDigitOfTotal = (evenTotal + oddTotal) % 10;
        return (10 - lastDigitOfTotal).ToString();
    }
}