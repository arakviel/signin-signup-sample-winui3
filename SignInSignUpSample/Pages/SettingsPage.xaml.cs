using System;
using System.Threading.Tasks;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace SignInSignUpSample.Pages;

public sealed partial class SettingsPage : Page
{

    public SettingsPage()
    {
        this.InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        base.OnNavigatedTo(e);
        InitializeSettings();
    }

    private void InitializeSettings()
    {
        // Встановлення поточної теми
        ElementTheme currentTheme = App.SettingsService.CurrentTheme;
        foreach (RadioButton radioButton in ThemeRadioButtons.Items)
        {
            if (radioButton.Tag.ToString() == currentTheme.ToString())
            {
                ThemeRadioButtons.SelectedItem = radioButton;
                break;
            }
        }

        // Встановлення поточної мови
        string currentLanguage = App.SettingsService.CurrentLanguage;
        foreach (RadioButton radioButton in LanguageRadioButtons.Items)
        {
            if (radioButton.Tag.ToString() == currentLanguage)
            {
                LanguageRadioButtons.SelectedItem = radioButton;
                break;
            }
        }
    }

    private void ThemeRadioButtons_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (ThemeRadioButtons.SelectedItem is not RadioButton selectedRadioButton) return;

        string themeString = selectedRadioButton.Tag.ToString()!;
        if (Enum.TryParse<ElementTheme>(themeString, out var theme))
        {
            // Змінюємо тему в налаштуваннях
            App.SettingsService.CurrentTheme = theme;

            // Застосування теми до поточної сторінки
            this.RequestedTheme = theme;
        }
    }

    private void LanguageRadioButtons_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (LanguageRadioButtons.SelectedItem is not RadioButton selectedRadioButton) return;

        string language = selectedRadioButton.Tag.ToString()!;
        if (language == App.SettingsService.CurrentLanguage) return;

        // Зберігаємо нову мову в налаштуваннях
        App.SettingsService.CurrentLanguage = language;

        // Змінюємо мову без перезавантаження
        App.Localizer.SetLanguage(language);

        // Показуємо повідомлення про успішну зміну мови
        SuccessInfoBar.IsOpen = true;

        // Автоматично закриваємо InfoBar через 3 секунди
        DispatcherQueue.TryEnqueue(async () =>
        {
            await Task.Delay(3000);
            SuccessInfoBar.IsOpen = false;
        });
    }


}