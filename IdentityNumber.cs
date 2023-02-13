using System.Text;
using System.Text.RegularExpressions;

namespace BirthdayExercise;

class IdentityNumber
{
    internal string id;
    public IdentityNumber(String id)
    {
        this.id = id;
    }

    /// <summary>
    /// Checks if ID number is valid if it has 13 digits and the last digit follows Luhn's Algorithm.
    /// </summary>
    /// <returns>Boolean</returns>
    public Boolean IsIdValid()
    {
        return IsIdStructureCorrect() && IsControlDigitValid();
    }

    public String ID { get { return id; } }

    /// <summary>
    /// Checks if a id(string) conforms to having a length of 13 characters with all being digits ranging from 0 to 9.
    /// </summary>
    /// <returns>Boolean</returns>
    private Boolean IsIdStructureCorrect()
    {
        return Regex.IsMatch(id, @"^\d{10}(0|1)\d{2}$");
    }

    /// <summary>
    /// Checks if last digit of id number is correct according to Luhn's algorithm
    /// </summary>
    /// <returns>Boolean</returns>
    private Boolean IsControlDigitValid()
    {
        int evenTotal = GetEvenIndexTotal();
        int oddTotal = GetOddIndexTotal();
        if (evenTotal>0 && oddTotal>0)
        {
            String expectedControlDigit = GetExpectedControlDigit();
            String actualControlDigit = id[12].ToString();
            return actualControlDigit.Equals(expectedControlDigit);
        }
        return false;
    }

    /// <summary>
    /// Adds all the digits at even indices in id number (0,2,4...12) and returns the sum
    /// </summary>
    /// <returns>int</returns>
    private int GetEvenIndexTotal()
    {
        StringBuilder stringBuilder = new StringBuilder();
        for (int i = 0; i < id.Length-1; i+=2)
        {
            stringBuilder.Append(id[i]);
        }

        return AddDigitsInString(stringBuilder.ToString());
    }


    /// <summary>
    /// Creates a new number from all the digits at odd indices in id number (1,3,5...11), multiplies the resultant number by two then returns the sum of the digits.
    /// </summary>
    /// <returns>int</returns>
    private int GetOddIndexTotal()
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
    /// <returns>int</returns>
    private int AddDigitsInString(String strNumber)
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
    /// <returns>String</returns>
    private String GetExpectedControlDigit()
    {
        int lastDigitOfTotal = (GetOddIndexTotal() + GetEvenIndexTotal()) % 10;
        return (10 - lastDigitOfTotal).ToString();
    }
}