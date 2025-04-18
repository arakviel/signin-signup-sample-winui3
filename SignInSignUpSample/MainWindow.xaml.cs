using Microsoft.UI.Xaml;
using SignInSignUpSample.Pages;

namespace SignInSignUpSample;

public sealed partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        ApplyTheme();
        SetupWindow();
    }

    private void SetupWindow()
    {
        var windowHandle = WinRT.Interop.WindowNative.GetWindowHandle(this);
        var windowId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(windowHandle);
        var appWindow = Microsoft.UI.Windowing.AppWindow.GetFromWindowId(windowId);

        appWindow.Resize(new Windows.Graphics.SizeInt32(800, 600));

        RootFrame.Navigate(typeof(LoginPage));
    }

    private void ApplyTheme()
    {
        // Застосування теми до вікна
        ElementTheme currentTheme = App.SettingsService.CurrentTheme;
        RootGrid.RequestedTheme = currentTheme;

        // Підписка на подію зміни теми
        App.SettingsService.ThemeChanged += (sender, theme) => RootGrid.RequestedTheme = theme;
    }
}
