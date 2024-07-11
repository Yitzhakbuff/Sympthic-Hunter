using System;
using static System.Console;
public static class Debugger
{
    public static void Debug(object? inf, string extr = null)
    {
        var additional = extr!=null ? $" [\x1b[38;5;91m{extr}\x1b[37m] |":"";
        WriteLine($"\x1b[38;5;28m{DateTime.UtcNow}\x1b[37m |{additional} [\x1b[38;5;239mDebug\x1b[37m]: {inf}");
    } 
    public static void Info(object? inf)
    {
        WriteLine($"\x1b[38;5;28m{DateTime.UtcNow}\x1b[37m | [\x1b[38;5;37mInfo\x1b[37m]: {inf}");
    } 
    public static void Error(object? inf)
    {
        WriteLine($"\x1b[38;5;28m{DateTime.UtcNow}\x1b[37m | [\x1b[38;5;9mError\x1b[37m]: {inf}");
    } 
}