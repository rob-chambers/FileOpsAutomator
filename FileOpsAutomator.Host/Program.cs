using System;
using System.Windows.Forms;
using System.Threading;
using System.Reflection;
using Ninject;
using FileOpsAutomator.Core;

namespace FileOpsAutomator.Host
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {            
            // Use the assembly GUID as the name of the mutex which we use to detect if an application instance is already running
            var mutexName = Assembly.GetExecutingAssembly().GetType().GUID.ToString();
            using (var mutex = new Mutex(false, mutexName, out var createdNew))
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
                    var kernel = new KernelConfiguration(new MainModule(), new CoreModule()).BuildReadonlyKernel();
                    var context = kernel.Get<STAApplicationContext>();
                    Application.Run(context);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
