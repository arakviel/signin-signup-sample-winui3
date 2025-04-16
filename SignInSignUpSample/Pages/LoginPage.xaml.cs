using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace SignInSignUpSample.Pages;

public sealed partial class LoginPage : Page
{
    public LoginPage()
    {
        this.InitializeComponent();
    }

    private void LoginButton_Click(object sender, RoutedEventArgs e)
    {
        // Очистити попередні повідомлення про помилки
        UsernameErrorTextBlock.Visibility = Visibility.Collapsed;
        InputPasswordErrorTextBlock.Visibility = Visibility.Collapsed;
        LoginErrorTextBlock.Visibility = Visibility.Collapsed;

        bool isValid = true;

        if (string.IsNullOrWhiteSpace(UsernameTextBox.Text))
        {
            UsernameErrorTextBlock.Text = "Логін не може бути порожнім";
            UsernameErrorTextBlock.Visibility = Visibility.Visible;
            isValid = false;
        }
        if (string.IsNullOrWhiteSpace(InputPasswordBox.Password))
        {
            InputPasswordErrorTextBlock.Text = "Пароль не може бути порожнім";
            InputPasswordErrorTextBlock.Visibility = Visibility.Visible;
            isValid = false;
        }

        if (!isValid)
            return;

        if (App.AuthService.Login(UsernameTextBox.Text, InputPasswordBox.Password))
            Frame.Navigate(typeof(MainPage));
        else
            LoginErrorTextBlock.Visibility = Visibility.Visible;
    }

    private void RegisterLink_Click(object sender, RoutedEventArgs e)
    {
        Frame.Navigate(typeof(RegisterPage));
    }
}
