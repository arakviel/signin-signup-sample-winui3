using System;
using Microsoft.UI.Xaml;
using Windows.Storage;

namespace SignInSignUpSample.Services;

public class SettingsService
{
    private const string LanguageKey = "Language";
    private const string ThemeKey = "Theme";
    private readonly ApplicationDataContainer _localSettings;

    public event EventHandler<string>? LanguageChanged;
    public event EventHandler<ElementTheme>? ThemeChanged;

    public SettingsService()
    {
        _localSettings = ApplicationData.Current.LocalSettings;

        // Встановлення значень за замовчуванням, якщо вони не існують
        if (!_localSettings.Values.ContainsKey(LanguageKey))
        {
            _localSettings.Values[LanguageKey] = "uk-UA";
        }

        if (!_localSettings.Values.ContainsKey(ThemeKey))
        {
            _localSettings.Values[ThemeKey] = ElementTheme.Default.ToString();
        }
    }

    public string CurrentLanguage
    {
        get => _localSettings.Values[LanguageKey]?.ToString() ?? "uk-UA";
        set
        {
            if (value != CurrentLanguage)
            {
                _localSettings.Values[LanguageKey] = value;
                LanguageChanged?.Invoke(this, value);
            }
        }
    }

    public ElementTheme CurrentTheme
    {
        get
        {
            string themeString = _localSettings.Values[ThemeKey]?.ToString() ?? ElementTheme.Default.ToString();
            return Enum.TryParse<ElementTheme>(themeString, out var theme) ? theme : ElementTheme.Default;
        }
        set
        {
            if (value != CurrentTheme)
            {
                // Зберігаємо тему в налаштуваннях
                _localSettings.Values[ThemeKey] = value.ToString();

                // Повідомляємо про зміну теми
                ThemeChanged?.Invoke(this, value);
            }
        }
    }
}
