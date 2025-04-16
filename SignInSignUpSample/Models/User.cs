namespace SignInSignUpSample.Models;

public class User
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string FullName { get; set; }

    public User()
    {

    }

    public User(string username, string password, string email, string fullName)
    {
        Username = username;
        Password = password;
        Email = email;
        FullName = fullName;
    }
}
