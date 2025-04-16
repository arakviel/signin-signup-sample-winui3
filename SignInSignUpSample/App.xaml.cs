using Microsoft.UI.Xaml;
using SignInSignUpSample.Services;


namespace SignInSignUpSample;

public partial class App : Application
{
    public static AuthService AuthService { get; private set; }

    private Window? window;

    public App()
    {
        InitializeComponent();
        AuthService = new AuthService();
    }

    protected override void OnLaunched(LaunchActivatedEventArgs args)
    {
        window = new MainWindow();
        window.Activate();
    }
}
