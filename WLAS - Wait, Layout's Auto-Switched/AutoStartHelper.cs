using Microsoft.Win32;
using System;
using System.Windows.Forms;

namespace WLAS___Wait__Layout_s_Auto_Switched
{
    public static class AutoStartHelper
    {
        private const string RegistryKeyPath = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";
        private const string AppName = "WLAS";

        public static bool IsEnabled()
        {
            using (var key = Registry.CurrentUser.OpenSubKey(RegistryKeyPath, false))
            {
                return key?.GetValue(AppName) != null;
            }
        }

        public static void Enable()
        {
            using (var key = Registry.CurrentUser.OpenSubKey(RegistryKeyPath, true))
            {
                string exePath = Application.ExecutablePath;
                key.SetValue(AppName, $"\"{exePath}\"");
            }
        }

        public static void Disable()
        {
            using (var key = Registry.CurrentUser.OpenSubKey(RegistryKeyPath, true))
            {
                if (key.GetValue(AppName) != null)
                    key.DeleteValue(AppName);
            }
        }
    }
}