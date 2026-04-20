using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Win32;

namespace FontFixerGUI
{
    public partial class Form1 : Form
    {
        private Label statusLabel;

        public Form1()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.FixedDialog;
this.MaximizeBox = false;
this.MinimizeBox = true;

            Text = "JP Font Fixer";
            Width = 360;
            Height = 240;
            Font = new Font("Segoe UI", 10);
            BackColor = Color.FromArgb(245, 245, 245);

            try
            {
                this.Icon = new Icon("icon.ico");
            }
            catch { /* ignore if missing */ }

            // STATUS LABEL
            statusLabel = new Label()
            {
                Text = "Status: Ready",
                Width = 300,
                Top = 10,
                Left = 20,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.Black
            };
            Controls.Add(statusLabel);

            // INSTALL BUTTON
            var installBtn = new Button()
            {
                Text = "Install Noto JP Fonts",
                Width = 260,
                Height = 40,
                Top = 50,
                Left = 40,
                FlatStyle = FlatStyle.Flat
            };

            installBtn.Click += (s, e) =>
            {
                if (FontsAlreadyInstalled())
                {
                    SetStatus("Already installed", Color.DarkGoldenrod);
                    Log("Install skipped");
                    return;
                }

                SetStatus("Installing...", Color.DarkOrange);
                Log("Starting install");

                RunScript("install.ps1");

                SetStatus("Install triggered", Color.DarkGreen);
                Log("Install script executed");
            };

            // REVERT BUTTON
            var revertBtn = new Button()
            {
                Text = "Revert Fonts",
                Width = 260,
                Height = 40,
                Top = 110,
                Left = 40,
                FlatStyle = FlatStyle.Flat
            };

            revertBtn.Click += (s, e) =>
            {
                SetStatus("Reverting...", Color.DarkOrange);
                Log("Starting revert");

                RunScript("revert.ps1");

                SetStatus("Reverted", Color.DarkGreen);
                Log("Revert script executed");
            };

            Controls.Add(installBtn);
            Controls.Add(revertBtn);
        }

        // STATUS HELPER
        private void SetStatus(string text, Color color)
        {
            statusLabel.Text = $"Status: {text}";
            statusLabel.ForeColor = color;
        }

        // RUN POWERSHELL SCRIPT
        private void RunScript(string scriptName)
        {
            var baseDir = AppDomain.CurrentDomain.BaseDirectory;
            var scriptPath = Path.GetFullPath(
                Path.Combine(baseDir, "..", "..", "..", "scripts", scriptName));

            if (!File.Exists(scriptPath))
            {
                MessageBox.Show($"Script not found:\n{scriptPath}");
                SetStatus("Script missing", Color.Red);
                Log($"Missing script: {scriptPath}");
                return;
            }

            var psi = new ProcessStartInfo
            {
                FileName = "powershell",
                Arguments = $"-ExecutionPolicy Bypass -File \"{scriptPath}\"",
                UseShellExecute = true,
                Verb = "runas"
            };

            Process.Start(psi);
        }

        // LOGGING
        private void Log(string message)
        {
            var logDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs");
            Directory.CreateDirectory(logDir);

            var file = Path.Combine(logDir, "app.log");
            File.AppendAllText(file, $"[{DateTime.Now}] {message}\n");
        }

        // SAFE MODE CHECK
        private bool FontsAlreadyInstalled()
        {
            try
            {
                var key = Registry.LocalMachine.OpenSubKey(
                    @"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts");

                if (key == null) return false;

                return key.GetValueNames().Any(v =>
                    v.Contains("Noto Sans JP"));
            }
            catch
            {
                return false;
            }
        }
    }
}