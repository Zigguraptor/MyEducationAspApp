using System.Diagnostics;

namespace MyEducationAspApp;

public static class BushExecutor
{
    public static string Execute(string command)
    {
        using var process = new Process();
        process.StartInfo.FileName = "/bin/bash";
        process.StartInfo.Arguments = $"-c \"{command}\"";
        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.RedirectStandardError = true;
        process.StartInfo.CreateNoWindow = true;
        process.StartInfo.UseShellExecute = false;

        process.Start();

        var output = process.StandardOutput.ReadToEnd();

        if (!process.WaitForExit(1000))
            process.Kill();

        return output;
    }
}
