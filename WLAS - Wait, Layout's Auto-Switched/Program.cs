using System;
using System.Text;
using System.Windows.Forms;

namespace WLAS___Wait__Layout_s_Auto_Switched
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }
    }
}