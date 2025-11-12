using System;
using System.Threading.Tasks;
using Firebase.Auth;
using UnityEngine;
public class Auth
{
    private static readonly FirebaseAuth firebaseAuth = FirebaseAuth.DefaultInstance;
    public static async Task<string> CreateNewAccount(string email, string password)
    {
        try
        {
            AuthResult authResult = await firebaseAuth.CreateUserWithEmailAndPasswordAsync(email, password);
            return authResult.User.UserId;
        }
        catch (TaskCanceledException)
        {
            Debug.Log("Account Creation was cancelled");
            return null;
        }
        catch (Exception exception)
        {
            Debug.Log($"Failed to create account\nException: {exception.Message}");
            return null;
        }
    }

    public static async Task<string> LoginAccount(string email, string password)
    {
        try
        {
            AuthResult authResult = await firebaseAuth.SignInWithEmailAndPasswordAsync(email, password);
            return authResult.User.UserId;
        }
        catch (TaskCanceledException)
        {
            Debug.Log("Account Login was cancelled");
            return null;
        }
        catch (Exception exception)
        {
            Debug.Log($"Failed to login account\nException: {exception.Message}");
            return null;
        }
    }
}
