using System;
using System.Collections.Generic;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;


namespace SignInSignUpSample.Pages;

public sealed partial class MainPage : Page
{
    private readonly Dictionary<string, Type> _pages = new()
    {
        { "home", typeof(HomePage) },
        { "info", typeof(InfoPage) },
        { "settings", typeof(SettingsPage) },
        { "profile", typeof(ProfilePage) }
    };

    public MainPage()
    {
        InitializeComponent();

        if (App.AuthService.CurrentUser == null)
            Frame.Navigate(typeof(LoginPage));
        else
        {
            ContentFrame.Navigate(typeof(HomePage));
            NavView.SelectedItem = NavView.MenuItems[0];
        }
    }

    private void NavView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
    {
        if (args.IsSettingsSelected)
        {
            NavView.Header = "Settings";
            ContentFrame.Navigate(typeof(SettingsPage));
            return;
        }

        if (args.SelectedItemContainer == null) return;

        string tag = args.SelectedItemContainer.Tag.ToString()!;
        NavView.Header = args.SelectedItemContainer.Content.ToString();

        if (_pages.TryGetValue(tag, out Type pageType)) ContentFrame.Navigate(pageType);
    }

    private void LogoutItem_Tapped(object sender, TappedRoutedEventArgs e)
    {
        App.AuthService.Logout();
        Frame.Navigate(typeof(LoginPage));
    }
}
