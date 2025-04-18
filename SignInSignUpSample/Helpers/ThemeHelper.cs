using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace SignInSignUpSample.Helpers;

public static class ThemeHelper
{
    public static void Initialize(Page page)
    {
        // Підписка на подію зміни теми
        App.SettingsService.ThemeChanged += (sender, theme) => page.RequestedTheme = theme;
        
        // Застосування поточної теми
        page.RequestedTheme = App.SettingsService.CurrentTheme;
    }
    
    public static void Cleanup(Page page)
    {
        // Відписка від події зміни теми
        App.SettingsService.ThemeChanged -= (sender, theme) => page.RequestedTheme = theme;
    }
}
