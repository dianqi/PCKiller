using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace PcLurker
{
    class pcControl
    {
        //电脑控制
        [DllImport("user32")]
        public static extern bool ExitWindowsEx(uint uFlags, uint dwReason);
        [DllImport("user32")]
        public static extern void LockWorkStation();
        [DllImport("user32")]
        public static extern int SendMessage(int hWnd, int hMsg, int wParam, int lParam);

        //隐藏桌面图标
        [DllImport("User32.dll", EntryPoint = "FindWindow")]
        public extern static IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", EntryPoint = "ShowWindow", SetLastError = true)]
        static extern bool ShowWindow(IntPtr hWnd, uint nCmdShow);

        public string status = "False";


        [DllImport("user32.dll")]
        private static extern int SendMessageA(int hWnd, int Msg, int wParam, int lParam);
        private const int HWND_BROADCAST = 0xffff;
        private const int WM_SYSCOMMAND = 0x112;
        private const int SC_SCREENSAVE = 0xf140;


        //修改电脑桌面
        [DllImport("user32.dll", EntryPoint = "SystemParametersInfo")]
        public static extern int SystemParametersInfo(
      int uAction,
      int uParam,
      string lpvParam,
      int fuWinIni
      );


        public enum MonitorState
        {
            MonitorStateOn = -1,
            MonitorStateOff = 2,
            MonitorStateStandBy = 1
        }
        /// <summary>
        /// 关机
        /// </summary>
        public static void ShutDown()
        {
            try
            {
                System.Diagnostics.ProcessStartInfo startinfo = new System.Diagnostics.ProcessStartInfo("shutdown.exe", "-s -t 00");
                System.Diagnostics.Process.Start(startinfo);
            }
            catch { }
        }
        /// <summary>
        /// 重启
        /// </summary>
        public static void Restart()
        {
            try
            {
                System.Diagnostics.ProcessStartInfo startinfo = new System.Diagnostics.ProcessStartInfo("shutdown.exe", "-r -t 00");
                System.Diagnostics.Process.Start(startinfo);
            }
            catch { }
        }/// <summary>
        /// 注销
        /// </summary>
        public static void LogOff()
        {
            try
            {
                ExitWindowsEx(0, 0);
            }
            catch { }
        }
        /// <summary>
        /// 锁定
        /// </summary>
        public static void LockPC()
        {
            try
            {
                LockWorkStation();
            }
            catch { }
        }
        /// <summary>
        ///关闭显示器 
        /// </summary>
        public static void Turnoffmonitor()
        {
            SetMonitorInState(MonitorState.MonitorStateOff);
        }

        /// <summary>
        /// 打开显示器
        /// </summary>
        public static void Turnonmonitor()
        {
            SetMonitorInState(MonitorState.MonitorStateOn);
        }

        private static void SetMonitorInState(MonitorState state)
        {
            SendMessage(0xFFFF, 0x112, 0xF170, (int)state);
        }


        public static void screenSaveStart()
        {
            SendMessageA(HWND_BROADCAST, WM_SYSCOMMAND, SC_SCREENSAVE, 0);
        }


        /// <summary>
        /// 隐藏任务栏和桌面图标
        /// </summary>
        public static void hideTaskbar()
        {
            IntPtr trayHwnd = FindWindow("Shell_TrayWnd", null);
            IntPtr hStar = FindWindow("Button", null);
            IntPtr desktopPtr = FindWindow("Progman", null);
            if (trayHwnd != IntPtr.Zero)
            {
                ShowWindow(desktopPtr, 0);//隐藏桌面图标 （0是隐藏，1是显示）
                ShowWindow(trayHwnd, 0);//隐藏任务栏
                ShowWindow(hStar, 0);//隐藏windows 按钮
            }
        }

        /// <summary>
        /// 显示任务栏和桌面图标 
        /// </summary>
        public static void showTaskbar()
        {
            IntPtr trayHwnd = FindWindow("Shell_TrayWnd", null);
            IntPtr hStar = FindWindow("Button", null);
            IntPtr desktopPtr = FindWindow("Progman", null);
            if (trayHwnd != IntPtr.Zero)
            {
                ShowWindow(desktopPtr, 1);
                ShowWindow(trayHwnd, 1);
                ShowWindow(hStar, 1);
            }
        }


        /// <summary>
        /// 修改电脑桌面背景
        /// </summary>
        public static void change()
        {
            string str = System.IO.Directory.GetCurrentDirectory();
            SystemParametersInfo(20, 0, "" + str + "" + "\\images\\sorry.jpg", 0x2);

             Bitmap bm = new Bitmap(@"images/sorry.jpg");
             bm.Save("sorry.bmp", System.Drawing.Imaging.ImageFormat.Bmp);
        }


    }
}
