using System.Text.RegularExpressions;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using SignInSignUpSample.Models;

namespace SignInSignUpSample.Pages;

public sealed partial class RegisterPage : Page
{
    public RegisterPage()
    {
        this.InitializeComponent();

        // Ініціалізація теми
        //ThemeHelper.Initialize(this);
    }

    private void RegisterButton_Click(object sender, RoutedEventArgs e)
    {
        // Очистити попередні повідомлення про помилки
        UsernameErrorTextBlock.Visibility = Visibility.Collapsed;
        EmailErrorTextBlock.Visibility = Visibility.Collapsed;
        FullNameErrorTextBlock.Visibility = Visibility.Collapsed;
        InputPasswordErrorTextBlock.Visibility = Visibility.Collapsed;
        ConfirmPasswordErrorTextBlock.Visibility = Visibility.Collapsed;
        RegisterErrorTextBlock.Visibility = Visibility.Collapsed;

        bool isValid = true;

        if (string.IsNullOrWhiteSpace(UsernameTextBox.Text))
        {
            UsernameErrorTextBlock.Text = "Логін не може бути порожнім";
            UsernameErrorTextBlock.Visibility = Visibility.Visible;
            isValid = false;
        }
        else if (UsernameTextBox.Text.Length < 3)
        {
            UsernameErrorTextBlock.Text = "Логін повинен містити не менше 3 символів";
            UsernameErrorTextBlock.Visibility = Visibility.Visible;
            isValid = false;
        }

        if (string.IsNullOrWhiteSpace(EmailTextBox.Text))
        {
            EmailErrorTextBlock.Text = "Email не можу бути порожнім";
            EmailErrorTextBlock.Visibility = Visibility.Visible;
            isValid = false;
        }
        else if (!IsValidEmail(EmailTextBox.Text))
        {
            EmailErrorTextBlock.Text = "Введіть коректний email";
            EmailErrorTextBlock.Visibility = Visibility.Visible;
            isValid = false;
        }

        if (string.IsNullOrWhiteSpace(FullNameTextBox.Text))
        {
            FullNameErrorTextBlock.Text = "Повне ім'я не може бути порожнім";
            FullNameErrorTextBlock.Visibility = Visibility.Visible;
            isValid = false;
        }

        if (string.IsNullOrWhiteSpace(InputPasswordBox.Password))
        {
            InputPasswordErrorTextBlock.Text = "Пароль не може бути порожнім";
            InputPasswordErrorTextBlock.Visibility = Visibility.Visible;
            isValid = false;
        }
        else if (InputPasswordBox.Password.Length < 6)
        {
            InputPasswordErrorTextBlock.Text = "Пароль повинен містити не менше 6 символів";
            InputPasswordErrorTextBlock.Visibility = Visibility.Visible;
            isValid = false;
        }

        if (InputPasswordBox.Password != ConfirmPasswordBox.Password)
        {
            ConfirmPasswordErrorTextBlock.Text = "Паролі не співпадають";
            ConfirmPasswordErrorTextBlock.Visibility = Visibility.Visible;
            isValid = false;
        }

        if (!isValid)
            return;

        User newUser = new User(
            UsernameTextBox.Text,
            InputPasswordBox.Password,
            EmailTextBox.Text,
            FullNameTextBox.Text
        );

        if (App.AuthService.Register(newUser))
            Frame.Navigate(typeof(MainPage));
        else
            RegisterErrorTextBlock.Visibility = Visibility.Visible;
    }

    private bool IsValidEmail(string text)
    {
        string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        return Regex.IsMatch(text, pattern);
    }

    private void LoginLink_Click(object sender, RoutedEventArgs e)
    {
        Frame.Navigate(typeof(LoginPage));
    }
}
