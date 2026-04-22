namespace FontFixerGUI;

using System;
using System.Security.Principal;
using static System.Net.Mime.MediaTypeNames;

static bool IsAdmin()
{
    var identity = WindowsIdentity.GetCurrent();
    var principal = new WindowsPrincipal(identity);
    return principal.IsInRole(WindowsBuiltInRole.Administrator);
}

[STAThread]
static void Main()
{
    if (!IsAdmin())
    {
        MessageBox.Show("Please run this app as Administrator.");
        return;
    }

    ApplicationConfiguration.Initialize();
    Application.Run(new Form1());
}