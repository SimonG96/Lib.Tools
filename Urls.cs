// Author: simon
// Created: 2024-09-27
// Copyright(c) 2024 SimonG. All Rights Reserved.

using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Lib.Tools;

public static class Urls
{
    public static void OpenUrlInBrowser(string url)
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            url = url.Replace("&", "^&");
            Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            Process.Start("xdg-open", url);
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            Process.Start("open", url);
        }
    }
}