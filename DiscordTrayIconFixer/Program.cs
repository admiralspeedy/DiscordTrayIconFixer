using Microsoft.Win32;
using System;
using System.Diagnostics;

namespace DiscordTrayIconFixer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Configure the process info
            ProcessStartInfo updateStartInfo = new ProcessStartInfo();
            updateStartInfo.FileName = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Discord\\Update.exe";
            updateStartInfo.Arguments = "--processStart Discord.exe";

            // Launch Discord through the Update executable
            try
            {
                using (Process updateProcess = Process.Start(updateStartInfo))
                {
                    updateProcess.WaitForExit(); // Wait for the update process to exit, so that the registry key is updated
                }
            } catch
            {
                Environment.Exit(1); // Just exit if the Update process fails to run
            }

            RegistryKey rootKey = Registry.CurrentUser.OpenSubKey("Control Panel\\NotifyIconSettings"); // Tray icon settings registry key

            // Search for the Discord key
            foreach(string subKeyName in rootKey.GetSubKeyNames())
            {
                using(RegistryKey subKey = rootKey.OpenSubKey(subKeyName, true))
                {
                    // If a Discord key is found, unhide the tray icon and break to exit
                    if (((string) subKey.GetValue("ExecutablePath", "")).Contains("Discord.exe"))
                    {
                        subKey.SetValue("IsPromoted", 1);
                        break;
                    }
                }
            }
        }
    }
}
