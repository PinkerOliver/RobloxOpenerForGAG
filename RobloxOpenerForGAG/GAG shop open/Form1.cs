using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace GAG_shop_open
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        class WindowHelper
        {
            [DllImport("user32.dll")]
            static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

            [DllImport("user32.dll")]
            static extern bool SetForegroundWindow(IntPtr hWnd);

            const int SW_RESTORE = 9;

            public static bool BringRobloxToFront()
            {
                Process[] processes = Process.GetProcessesByName("RobloxPlayerBeta");

                if (processes.Length == 0)
                    return false;

                IntPtr hWnd = processes[0].MainWindowHandle;

                if (hWnd == IntPtr.Zero)
                    return false;

                ShowWindow(hWnd, SW_RESTORE);
                SetForegroundWindow(hWnd);

                return true;
            }
        }
        public void Mincheck() 
        {
            while (true)
            {
                DateTime now = DateTime.Now;

                int minutesToNext = 5 - (now.Minute % 5);
                if (minutesToNext == 5)
                    minutesToNext = 0;

                DateTime next = new DateTime(
                    now.Year, now.Month, now.Day,
                    now.Hour, now.Minute, 0)
                    .AddMinutes(minutesToNext);

                if (next <= now)
                    next = next.AddMinutes(5);

                Thread.Sleep(next - now);
                if (!WindowHelper.BringRobloxToFront())
                {
                    Debug.WriteLine("Roblox is not running.");
                }


            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            MessageBox.Show("This program will bring Roblox to the front every 5 minutes. Please keep this window open.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Task.Run(() => Mincheck());
        }
    }
}
