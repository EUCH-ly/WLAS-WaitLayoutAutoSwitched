using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace WLAS___Wait__Layout_s_Auto_Switched
{
    public static class KeyboardLayoutHelper
    {
        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        private static extern uint GetWindowThreadProcessId(IntPtr hWnd, IntPtr ProcessId);

        [DllImport("user32.dll")]
        private static extern IntPtr GetKeyboardLayout(uint idThread);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr LoadKeyboardLayout(string pwszKLID, uint Flags);

        [DllImport("user32.dll")]
        private static extern IntPtr PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        private const uint WM_INPUTLANGCHANGEREQUEST = 0x0050;
        private const string RU_LAYOUT_ID = "00000419";
        private const string EN_LAYOUT_ID = "00000409";

        public static bool IsCurrentLayoutRussian()
        {
            IntPtr foregroundWindow = GetForegroundWindow();
            uint threadId = GetWindowThreadProcessId(foregroundWindow, IntPtr.Zero);
            IntPtr layout = GetKeyboardLayout(threadId);

            int langId = layout.ToInt32() & 0xFFFF;
            return langId == 0x0419;
        }

        public static void SwitchToRussian()
        {
            SwitchLayout(RU_LAYOUT_ID);
        }

        public static void SwitchToEnglish()
        {
            SwitchLayout(EN_LAYOUT_ID);
        }

        private static void SwitchLayout(string layoutId)
        {
            IntPtr hkl = LoadKeyboardLayout(layoutId, 0x00000001); // KLF_ACTIVATE
            IntPtr foregroundWindow = GetForegroundWindow();

            // Отправляем именно активному окну запрос на смену раскладки
            PostMessage(foregroundWindow, WM_INPUTLANGCHANGEREQUEST, IntPtr.Zero, hkl);
        }

        [DllImport("user32.dll")]
        private static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);

        private const uint KEYEVENTF_KEYUP = 0x0002;
        private const byte VK_BACK = 0x08;
        private const byte VK_CAPITAL = 0x14;
        private const byte VK_SHIFT = 0x10;

        [DllImport("user32.dll")]
        private static extern short GetKeyState(int nVirtKey);

        public static void SendBackspaces(int count)
        {
            for (int i = 0; i < count; i++)
            {
                keybd_event(VK_BACK, 0, 0, UIntPtr.Zero);
                keybd_event(VK_BACK, 0, KEYEVENTF_KEYUP, UIntPtr.Zero);
                System.Threading.Thread.Sleep(15);
            }
        }

        public static void TypeText(string text)
        {
            foreach (char c in text)
            {
                SendKeys.SendWait(EscapeForSendKeys(c));
            }
        }

        private static string EscapeForSendKeys(char c)
        {
            if ("+^%~(){}[]".IndexOf(c) >= 0)
                return "{" + c + "}";
            return c.ToString();
        }

        public static void ResetModifiers()
        {
            if ((GetKeyState(VK_CAPITAL) & 0x0001) != 0)
            {
                keybd_event(VK_CAPITAL, 0, 0, UIntPtr.Zero);
                keybd_event(VK_CAPITAL, 0, KEYEVENTF_KEYUP, UIntPtr.Zero);
            }

            keybd_event(VK_SHIFT, 0, KEYEVENTF_KEYUP, UIntPtr.Zero);
        }
    }
}