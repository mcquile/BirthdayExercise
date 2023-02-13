namespace BirthdayExercise;

class BirthdayUtility
{
    /// <summary>
    /// Checks if nullable DateTime argument passed is before the current date and is not null.
    /// </summary>
    /// <param name="birthday">DateTime nullable of birthday</param>
    /// <returns>Boolean</returns>
    internal static Boolean IsBirthdayInThePast(DateTime birthday)
    {
        return DateTime.Now.CompareTo(birthday) > 0;
    }

    /// <summary>
    /// Checks if the DateTime argument passed is before or after 01/01/2010. Updates the relevent integer and returns the values.
    /// </summary>
    /// <param name="birthday">DateTime object containing date to compare</param>
    /// <param name="before2010">int used to track how many dates are before 01/01/2010</param>
    /// <param name="after2010">int used to track how many dates are after 01/01/2010</param>
    /// <returns></returns>
    internal static void IsBirthdayAfter2010(DateTime birthday, ref int before2010, ref int after2010)
    {
        DateTime newYears2010 = new DateTime(2010, 01, 01);
        (before2010, after2010) = birthday.CompareTo(newYears2010) > 0 ?  (before2010, after2010+1) : (before2010+1, after2010);
    }
}