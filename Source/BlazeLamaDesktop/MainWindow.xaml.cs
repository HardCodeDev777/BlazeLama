using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace BlazeLamaDesktop;

public partial class MainWindow : Window
{
    private Process? blazorProcess, ollamaProcess;

    public MainWindow()
    {
        InitializeComponent();
        StartBlazor();
    }

    private async void StartBlazor()
    {
        var blazorPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "BlazeLamaWeb.exe");
        var ollamaPath = await ReadEXEPath.ReadFromJSONPath();

        if (File.Exists(blazorPath))
        {
            var startInfoBlazor = new ProcessStartInfo{ FileName = blazorPath, UseShellExecute = false };

            startInfoBlazor.EnvironmentVariables["ASPNETCORE_ENVIRONMENT"] = "Production";

            blazorProcess = new Process { StartInfo = startInfoBlazor };

            blazorProcess.Start();

            if (!string.IsNullOrEmpty(ollamaPath))
            {
                var startInfoOllama = new ProcessStartInfo { FileName = ollamaPath, UseShellExecute = false };
                ollamaProcess = new Process { StartInfo = startInfoOllama };

                ollamaProcess.Start();
            }
        }

        await webView.EnsureCoreWebView2Async(null);
        webView.Source = new System.Uri("http://localhost:5000");
    }

    protected override void OnClosed(System.EventArgs e)
    {
        blazorProcess?.Kill();
        ollamaProcess?.Kill();
  
        base.OnClosed(e);
    }

    private void TitleBar_MouseDown(object sender, MouseButtonEventArgs e)
    {
        if (e.ChangedButton == MouseButton.Left)this.DragMove();
    }

    private void CloseButton_Click(object sender, RoutedEventArgs e) => this.Close();
        

    private void MinimizeButton_Click(object sender, RoutedEventArgs e) => this.WindowState = WindowState.Minimized;
        

    private void MaxRestoreButton_Click(object sender, RoutedEventArgs e)
    {
        if (this.WindowState == WindowState.Normal) this.WindowState = WindowState.Maximized;
        else this.WindowState = WindowState.Normal;
    }

}
