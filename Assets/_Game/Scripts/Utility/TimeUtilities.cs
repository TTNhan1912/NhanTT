using System;

public class TimeUtilities
{
    public static DateTime ConvertTimeString(string timeString)
    {
        return DateTime.Parse(timeString);
    }
}