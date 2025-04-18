using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace SignInSignUpSample.Pages;
public sealed partial class ProfilePage : Page
{
    public ProfilePage()
    {
        InitializeComponent();
        LoadUserData();

        // Ініціалізація теми
        //ThemeHelper.Initialize(this);
    }

    protected override void OnNavigatedFrom(NavigationEventArgs e)
    {
        base.OnNavigatedFrom(e);
    }

    private void LoadUserData()
    {
        var currentUser = App.AuthService.CurrentUser;

        if (currentUser == null) Frame.Navigate(typeof(MainPage));

        UserNameTextBlock.Text = currentUser!.Username;
        EmailTextBlock.Text = currentUser.Email;
        FullNameTextBlock.Text = currentUser.FullName;

        ProfilePicture.DisplayName = currentUser.FullName;
    }
}
