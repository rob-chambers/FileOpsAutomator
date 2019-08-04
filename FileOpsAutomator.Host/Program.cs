using System;
using System.Windows.Forms;
using System.Threading;
using System.Reflection;

namespace FileOpsAutomator.Host
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {            
            // Use the assembly GUID as the name of the mutex which we use to detect if an application instance is already running
            var createdNew = false;
            var mutexName = Assembly.GetExecutingAssembly().GetType().GUID.ToString();
            using (var mutex = new Mutex(false, mutexName, out createdNew))
            {
                if (!createdNew)
                {
                    // Only allow one instance
                    return;
                }

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                try
                {
                    var context = new STAApplicationContext();
                    Application.Run(context);
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message, "Error");
                }
            }
        }
    }
}
