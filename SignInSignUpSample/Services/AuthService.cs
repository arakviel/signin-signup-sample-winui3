namespace SignInSignUpSample.Services;

using System;
using System.Collections.Generic;
using SignInSignUpSample.Models;

public class AuthService
{
    private List<User> _users;
    private User _currentUser;

    public User CurrentUser => _currentUser;

    public AuthService()
    {
        _users = new List<User>()
        {
            new User("demo", "Password123$", "demo@example.com", "Demo User"),
        };
    }

    // TODO: верифікація через хешування
    public bool Login(string username, string password)
    {
        foreach (var user in _users)
        {
            if (user.Username.Equals(username, StringComparison.OrdinalIgnoreCase) && user.Password == password)
            {
                _currentUser = user;
                return true;
            }
        }

        return false;
    }

    // TODO: так само і для пошти. Також додати збереження пароля захешованого
    public bool Register(User newUser)
    {
        foreach (var user in _users)
        {
            if (user.Username.Equals(newUser.Username, StringComparison.OrdinalIgnoreCase))
            {
                return false; // Username already exists
            }
        }
        _users.Add(newUser);
        _currentUser = newUser;
        return true;
    }

    public void Logout()
    {
        _currentUser = null;
    }
}