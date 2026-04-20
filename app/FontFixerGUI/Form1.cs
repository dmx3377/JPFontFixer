using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace FontFixerGUI
{
    public partial class Form1 : Form
    {
        private Label statusLabel;
        private TextBox logBox;
        private Button installBtn;
        private Button revertBtn;
        private ComboBox langBox;

        public Form1()
        {
            InitializeComponent();

            Text = "JP Font Fixer v1.1";
            Width = 360;
            Height = 320;

            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;

            try
            {
                Icon = new Icon("icon.ico");
            }
            catch { }

            // STATUS
            statusLabel = new Label()
            {
                Text = "Status: Idle",
                Top = 10,
                Left = 20,
                Width = 300
            };

            // INSTALL BUTTON
            installBtn = new Button()
            {
                Text = "Install Fonts",
                Width = 280,
                Top = 40,
                Left = 20
            };

            // REVERT BUTTON
            revertBtn = new Button()
            {
                Text = "Revert Fonts",
                Width = 280,
                Top = 80,
                Left = 20
            };

            // LANGUAGE SELECT
            langBox = new ComboBox()
            {
                Top = 120,
                Left = 20,
                Width = 140
            };

            langBox.Items.AddRange(new[] { "English", "日本語" });
            langBox.SelectedIndex = 0;
            langBox.SelectedIndexChanged += (s, e) => SwitchLanguage();

            // LOG BOX
            logBox = new TextBox()
            {
                Top = 160,
                Left = 20,
                Width = 300,
                Height = 100,
                Multiline = true,
                ReadOnly = true,
                ScrollBars = ScrollBars.Vertical
            };

            // EVENTS
            installBtn.Click += async (s, e) => await RunScript("install.ps1");
            revertBtn.Click += async (s, e) => await RunScript("revert.ps1");

            // ADD CONTROLS
            Controls.Add(statusLabel);
            Controls.Add(installBtn);
            Controls.Add(revertBtn);
            Controls.Add(langBox);
            Controls.Add(logBox);

            // ADMIN CHECK
            if (!IsAdmin())
            {
                MessageBox.Show(
                    "This tool requires Administrator privileges.",
                    "JP Font Fixer",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }
        }

        private async Task RunScript(string scriptName)
        {
            SetStatus("Running...");
            SetButtons(false);
            Log($"Starting {scriptName}");

            try
            {
                var baseDir = AppDomain.CurrentDomain.BaseDirectory;
                var scriptPath = Path.Combine(baseDir, "scripts", scriptName);

                if (!File.Exists(scriptPath))
                {
                    SetStatus("Error ❌");
                    Log("Script not found: " + scriptPath);
                    SetButtons(true);
                    return;
                }

                var psi = new ProcessStartInfo
                {
                    FileName = "powershell",
                    Arguments = $"-ExecutionPolicy Bypass -File \"{scriptPath}\"",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                };

                var process = Process.Start(psi);

                string output = await process.StandardOutput.ReadToEndAsync();
                string error = await process.StandardError.ReadToEndAsync();

                process.WaitForExit();

                if (!string.IsNullOrWhiteSpace(output))
                    Log(output);

                if (!string.IsNullOrWhiteSpace(error))
                {
                    Log("ERROR: " + error);
                    SetStatus("Error ❌");
                }
                else
                {
                    Log("Done ✔");
                    SetStatus("Completed ✔");
                }
            }
            catch (Exception ex)
            {
                Log("Exception: " + ex.Message);
                SetStatus("Error ❌");
            }

            SetButtons(true);
        }

        private void SetStatus(string text)
        {
            statusLabel.Text = "Status: " + text;
        }

        private void Log(string message)
        {
            logBox.AppendText($"[{DateTime.Now:T}] {message}{Environment.NewLine}");
        }

        private void SetButtons(bool enabled)
        {
            installBtn.Enabled = enabled;
            revertBtn.Enabled = enabled;
        }

        private bool IsAdmin()
        {
            var identity = WindowsIdentity.GetCurrent();
            var principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }

        private void SwitchLanguage()
        {
            bool jp = langBox.SelectedIndex == 1;

            installBtn.Text = jp ? "フォントをインストール" : "Install Fonts";
            revertBtn.Text = jp ? "元に戻す" : "Revert Fonts";
            Text = jp ? "日本語フォント修正ツール v1.1" : "JP Font Fixer v1.1";
        }
    }
}