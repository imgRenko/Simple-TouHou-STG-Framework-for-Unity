using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using UnityEngine;
[AddComponentMenu("东方STG框架/框架核心/IME屏蔽器")]
public class Win32Help
{
    private delegate bool Wndenumproc(IntPtr hwnd, uint lParam);

    [DllImport("user32.dll", SetLastError = true)]
    private static extern bool EnumWindows(Wndenumproc lpEnumFunc, uint lParam);

    [DllImport("user32.dll", SetLastError = true)]
    private static extern IntPtr GetParent(IntPtr hWnd);
    [DllImport("user32.dll")]
    private static extern uint GetWindowThreadProcessId(IntPtr hWnd, ref uint lpdwProcessId);

    [DllImport("kernel32.dll")]
    private static extern void SetLastError(uint dwErrCode);


    /// <summary>
    /// 获取当前进程的窗口句柄
    /// </summary>
    /// <returns></returns>
    public static IntPtr GetProcessWnd()
    {
        var ptrWnd = IntPtr.Zero;
        var pid = (uint)Process.GetCurrentProcess().Id;  // 当前进程 ID  

        var bResult = EnumWindows(delegate (IntPtr hwnd, uint lParam)
        {
            uint id = 0;

            if (GetParent(hwnd) != IntPtr.Zero)
                return true;
            GetWindowThreadProcessId(hwnd, ref id);
            if (id != lParam)
                return true;
            ptrWnd = hwnd;   // 把句柄缓存起来  
            SetLastError(0);    // 设置无错误  
            return false;   // 返回 false 以终止枚举窗口  
        }, pid);

        return (!bResult && Marshal.GetLastWin32Error() == 0) ? ptrWnd : IntPtr.Zero;
    }

    [DllImport("imm32.dll")]
    private static extern IntPtr ImmGetContext(IntPtr hwnd);
    [DllImport("imm32.dll")]
    private static extern bool ImmGetOpenStatus(IntPtr himc);
    [DllImport("imm32.dll")]
    private static extern bool ImmSetOpenStatus(IntPtr himc, bool b);

    /// <summary>
    /// 设置输入法状态
    /// </summary>
    /// <param name="tf"></param>
    public static void SetImeEnable(bool tf)
    {
        var handle = GetProcessWnd();
        var hIme = ImmGetContext(handle);
        ImmSetOpenStatus(hIme, tf);
    }

    /// <summary>
    /// 获取输入法状态
    /// </summary>
    /// <returns></returns>
    public bool GetImeStatus()
    {
        var handle = GetProcessWnd();
        var hIme = ImmGetContext(handle);
        return ImmGetOpenStatus(hIme);
    }
}