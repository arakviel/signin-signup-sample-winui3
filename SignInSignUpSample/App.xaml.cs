using System;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using Microsoft.UI.Xaml;
using SignInSignUpSample.Services;
using Windows.Storage;
using WinUI3Localizer;

namespace SignInSignUpSample;

public partial class App : Application
{
    public static AuthService AuthService { get; private set; } = null!;
    public static SettingsService SettingsService { get; private set; } = null!;
    public static ILocalizer Localizer { get; private set; } = null!;

    private Window? window;

    public App()
    {
        InitializeComponent();
        AuthService = new AuthService();
        SettingsService = new SettingsService();

        // Ініціалізація локалізатора
        InitializeLocalizer();
    }

    private async Task InitializeLocalizer()
    {
        // Встановлення поточної мови
        string language = SettingsService.CurrentLanguage;

        CultureInfo.CurrentCulture = new CultureInfo(language);
        CultureInfo.CurrentUICulture = new CultureInfo(language);

        // Initialize a "Strings" folder in the executables folder.
        var StringsFolderPath = Path.Combine(AppContext.BaseDirectory, "Strings");
        StorageFolder stringsFolder = await StorageFolder.GetFolderFromPathAsync(StringsFolderPath);

        Localizer = await new LocalizerBuilder()
            .AddStringResourcesFolderForLanguageDictionaries(StringsFolderPath)
            .SetOptions(options =>
            {
                options.DefaultLanguage = language;
            })
            .Build();
    }

    protected override void OnLaunched(LaunchActivatedEventArgs args)
    {
        window = new MainWindow();
        window.Activate();
    }
}
