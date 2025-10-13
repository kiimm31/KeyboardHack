using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;

namespace KeyboardHackerMan.Helper
{
    public static class WindowsHelper
    {
        [DllImport("user32.dll")]
        internal static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        [DllImport("user32.dll")]
        internal static extern IntPtr SetForegroundWindow(IntPtr hWnd);

        public static void FocusWindow(string processName)
        {
            var targetProcess = Process.GetProcessesByName(processName);
            var handler = targetProcess.First().MainWindowHandle;
            WindowsHelper.ShowWindow(handler, 9);
            WindowsHelper.SetForegroundWindow(handler);
        }

        public static void FocusWindow(Process processName)
        {
            var handler = processName.MainWindowHandle;
            WindowsHelper.ShowWindow(handler, 9);
            WindowsHelper.SetForegroundWindow(handler);
        }
    }
}
