using System;
using System.Runtime;
using System.Runtime.InteropServices;
using System.Diagnostics;
using NetOnOff;
using System.Net;

ConfigManager.Initialize();

try
{
    ProcessStartInfo rasdial = new ProcessStartInfo(Environment.ExpandEnvironmentVariables("%systemroot%") + @"\system32\rasdial.exe");

    if (CheckForInternetConnection())
    {
        rasdial.Arguments = $"{ConfigManager.name} /disconnect";
        //Console.WriteLine(1);
    }
    else
    {
        rasdial.Arguments = $"{ConfigManager.name} {ConfigManager.login} {ConfigManager.password}";
        //Console.WriteLine(2);
    }

    Process.Start(rasdial);
}
catch (Exception ex)
{
    Console.WriteLine(ex.ToString());
    ConfigManager.SomethingWentWrong();
}
finally
{
    Environment.Exit(0);
}

bool CheckForInternetConnection()
{
    try
    {
        using (var client = new WebClient())
        using (var stream = client.OpenRead("http://www.google.com"))
        {
            return true;
        }
    }
    catch
    {
        return false;
    }
}