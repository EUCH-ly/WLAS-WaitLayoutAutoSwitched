using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace WLAS___Wait__Layout_s_Auto_Switched
{
    public class KeyboardHook
    {
        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;

        private LowLevelKeyboardProc _proc;
        private IntPtr _hookID = IntPtr.Zero;

        public event Action<char> OnCharTyped;
        public event Action OnSpacePressed;
        public event Action OnBackspacePressed;
        public event Action OnBufferReset; // новое событие — сброс буфера на "непонятной" клавише

        public bool IsPaused { get; set; } = false;

        // Для фильтрации повторных KEYDOWN от удержания клавиши
        private Keys _lastKey = Keys.None;
        private DateTime _lastKeyTime = DateTime.MinValue;

        public KeyboardHook()
        {
            _proc = HookCallback;
        }

        public void Start()
        {
            _hookID = SetHook(_proc);
        }

        public void Stop()
        {
            UnhookWindowsHookEx(_hookID);
        }

        private IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (var curProcess = System.Diagnostics.Process.GetCurrentProcess())
            using (var curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_KEYBOARD_LL, proc,
                    GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        private IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (IsPaused)
            {
                return CallNextHookEx(_hookID, nCode, wParam, lParam);
            }

            if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)
            {
                int vkCode = Marshal.ReadInt32(lParam);
                Keys key = (Keys)vkCode;

                // Защита от дублей при удержании клавиши (авто-повтор)
                var now = DateTime.Now;
                bool isDuplicate = key == _lastKey && (now - _lastKeyTime).TotalMilliseconds < 60;
                _lastKey = key;
                _lastKeyTime = now;

                if (key == Keys.Space)
                {
                    OnSpacePressed?.Invoke();
                }
                else if (key == Keys.Back)
                {
                    if (!isDuplicate)
                        OnBackspacePressed?.Invoke();
                }
                else if (key >= Keys.A && key <= Keys.Z)
                {
                    if (!isDuplicate)
                        OnCharTyped?.Invoke((char)('a' + (key - Keys.A)));
                }
                else
                {
                    // Любая другая клавиша (стрелки, Delete, Enter, цифры, Ctrl-комбинации и т.д.)
                    // считаем, что контекст слова нарушен — сбрасываем буфер
                    OnBufferReset?.Invoke();
                }
            }

            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }

        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);
    }
}