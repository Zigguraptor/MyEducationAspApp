namespace MyEducationAspApp;

public static class Time
{
    public static string GetTimeStamp()
    {
        return DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();
    }
}
