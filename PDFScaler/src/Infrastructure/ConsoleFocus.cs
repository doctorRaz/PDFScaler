﻿using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace drz.PDFScaler.Infrastructure
{
    /// <summary>
    /// Фозвращает фокус консоли
    /// </summary>
    internal class ConsoleFocus
    {
        [DllImport("user32.dll")]
        public static extern bool ShowWindowAsync(HandleRef hWnd, int nCmdShow);
        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr WindowHandle);

        public const int SW_RESTORE = 9;
        /// <summary>
        /// Set focus
        /// </summary>
        /// <param name="procName"></param>
        public static void FocusProcess(string procName)
        {
            Process[] objProcesses = System.Diagnostics.Process.GetProcessesByName(procName);
            if (objProcesses.Length > 0)
            {
                IntPtr hWnd = IntPtr.Zero;
                hWnd = objProcesses[0].MainWindowHandle;
                ShowWindowAsync(new HandleRef(null, hWnd), SW_RESTORE);
                SetForegroundWindow(objProcesses[0].MainWindowHandle);
            }
        }


    }
}
