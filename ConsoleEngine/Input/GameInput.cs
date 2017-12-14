using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;

namespace ConsoleEngine.Input
{
    public static class InputManager
    {
        public enum InputCheckState
        {
            enable, disable
        }
        public static InputCheckState inputCheckState = InputCheckState.enable;
        public static Action<Keys> OnKeyDownEvent = delegate { };
        public static Action<Keys> OnKeyPressEvent = delegate { };
        public static Action<Keys> OnKeyUpEvent = delegate { };
        private static Keys downKey; //Нажатая клавиша
        private static Keys upKey; //Отпущенная клавиша
        private static List<Keys> pressedKeys; //Список нажатых клавиш
        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100; //Код нажатой клавиши
        private const int WM_KEYUP = 0x0101; //Код поднятой клавиши
        private static LowLevelKeyboardProc _proc = HookCallback; //Кто ловить собирается
        private static IntPtr _hookID = IntPtr.Zero; //Ид хука
        public static void Init() //Метод инициализации управления
        {
            _hookID = SetHook(_proc); //Ставим хук
            pressedKeys = new List<Keys>(); //Делаем массив нажатых клавиш
        }
        public static bool GetKeyDown(Keys k) //Когда нажата клавиша
        {
            bool b = (k == downKey);
            downKey = Keys.None;
            return b;
        }
        public static bool GetKeyUp(Keys k) //Когда клавиша отпущена
        {
            bool b = (k == upKey);
            downKey = Keys.None;
            return b;
        }
        public static bool GetKeyPress(Keys k) //Когда клавиша нажата
        {
            return (pressedKeys.Contains(k));
        }
        private static IntPtr SetHook(LowLevelKeyboardProc proc) //Низкоуровневый метод установления хука
        {
            using (Process curProcess = Process.GetCurrentProcess())
            {
                using (ProcessModule curModule = curProcess.MainModule)
                {
                    return SetWindowsHookEx(WH_KEYBOARD_LL, proc, GetModuleHandle(curModule.ModuleName), 0);
                }
            }
        }
        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);
        private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam) //Сам метод, вызывающийся при действиях клавиатуры
        {
            if (!ConsoleEngine.Editor.GameEditorForm.editorFocused) //Если мы не сфокусированы на едиторе(поменять)
            {
                const int HC_ACTION = 0;
                if (nCode == HC_ACTION)
                {
                    Keys key = (Keys)Marshal.ReadInt32(lParam); //Парсим нажатую клавишу в Keys
                    if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN) //Если клавиша только нажата
                    {
                        if (!pressedKeys.Contains(key)) //Если у нас такой клавиши ещё не нажимали
                        {
                            downKey = key; //Делаем downKey равным текущей клавише
                            pressedKeys.Add(key); //И добавляем в массив нажатых клавиш текущую
                        }
                    }
                    else if (nCode >= 0 && wParam == (IntPtr)WM_KEYUP) //Если же клавиша отпущена
                    {
                        upKey = key; //Отпущенная клавиша становится текущей
                        pressedKeys.Remove(key); //И удаляем эту клавишу из списка нажатых клавиш
                    }
                }
            }
            return CallNextHookEx(_hookID, nCode, wParam, lParam); //Вызываем следующий хук
        }
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
