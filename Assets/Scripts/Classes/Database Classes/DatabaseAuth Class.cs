using System;
using System.Threading.Tasks;
using Firebase;
using Firebase.Auth;
using UnityEngine;
public class Auth
{
    private static readonly FirebaseAuth firebaseAuth = FirebaseAuth.DefaultInstance;
    public static async Task<AccountAuthResult> CreateNewAccountAsync(string email, string password)
    {
        AccountAuthResult accountAuthResult = new AccountAuthResult();
        try
        {
            AuthResult authResult = await firebaseAuth.CreateUserWithEmailAndPasswordAsync(email, password);
            accountAuthResult.userID = authResult.User.UserId;
            Debug.Log("Account Creation Successful");
            return accountAuthResult;
        }
        catch (TaskCanceledException)
        {
            Debug.Log("Account Creation was cancelled");
            accountAuthResult.isCancelled = true;
            return accountAuthResult;
        }
        catch (Exception exception)
        {
            FirebaseException firebaseException = exception.GetBaseException() as FirebaseException;
            AuthError authError = (AuthError)firebaseException.ErrorCode;
            accountAuthResult.isFaulted = true;
            accountAuthResult.errorCode = authError;
            Debug.Log($"Failed to create account\nException: {exception.Message}");
            return accountAuthResult;
        }
    }

    public static async Task<AccountAuthResult> LoginAccountAsync(string email, string password)
    {
        AccountAuthResult accountAuthResult = new AccountAuthResult();
        try
        {
            AuthResult authResult = await firebaseAuth.SignInWithEmailAndPasswordAsync(email, password);
            accountAuthResult.userID = authResult.User.UserId;
            Debug.Log("Login Successful");
            return accountAuthResult;
        }
        catch (TaskCanceledException)
        {
            Debug.Log("Account Login was cancelled");
            accountAuthResult.isCancelled = true;
            return accountAuthResult;
        }
        catch (Exception exception)
        {
            FirebaseException firebaseException = exception.GetBaseException() as FirebaseException;
            AuthError authError = (AuthError)firebaseException.ErrorCode;
            accountAuthResult.isFaulted = true;
            accountAuthResult.errorCode = authError;
            Debug.Log($"Failed to login account\nException: {exception.Message}");
            return accountAuthResult;
        }
    }
}

public class AccountAuthResult
{
    public bool isFaulted = false;
    public bool isCancelled = false;
    public AuthError errorCode;
    public string userID;
}
