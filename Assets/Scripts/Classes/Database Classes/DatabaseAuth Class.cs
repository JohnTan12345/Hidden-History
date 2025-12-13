//-----------------------------------------------------------------------------------------------------------------
// Created By: John Tan
// Description: Firebase Authentication Handler
//-----------------------------------------------------------------------------------------------------------------

using System.Threading.Tasks;
using Firebase;
using Firebase.Auth;
using UnityEngine;
public class Auth
{
    private static readonly FirebaseAuth firebaseAuth = FirebaseAuth.DefaultInstance;
    public static async Task<AccountAuthResult> CreateNewAccountAsync(string email, string password)
    {
        AccountAuthResult accountAuthResult = new AccountAuthResult(); // Make a return result object
        try
        {
            AuthResult authResult = await firebaseAuth.CreateUserWithEmailAndPasswordAsync(email, password);
            accountAuthResult.userID = authResult.User.UserId; // Assign UID if successful
            Debug.Log("Account Creation Successful");
            return accountAuthResult;
        }
        catch (TaskCanceledException)
        {
            Debug.Log("Account Creation was cancelled");
            accountAuthResult.isCancelled = true; // Set task cancelling as reason for error
            return accountAuthResult;
        }
        catch (FirebaseException exception)
        {
            AuthError authError = (AuthError)exception.ErrorCode; // Get the error code
            accountAuthResult.isFaulted = true; // Set firebase error as reason for error
            accountAuthResult.errorCode = authError; // Pass on the error code
            Debug.Log($"Failed to create account\nRaw Message: {exception.Message}\nError Code: {authError}");
            return accountAuthResult;
        }
    }

    public static async Task<AccountAuthResult> LoginAccountAsync(string email, string password)
    {
        AccountAuthResult accountAuthResult = new AccountAuthResult();
        try
        {
            AuthResult authResult = await firebaseAuth.SignInWithEmailAndPasswordAsync(email, password);
            accountAuthResult.userID = authResult.User.UserId; // Assign UID if successful
            Debug.Log("Login Successful");
            return accountAuthResult;
        }
        catch (TaskCanceledException)
        {
            Debug.Log("Account Login was cancelled");
            accountAuthResult.isCancelled = true; // Set task cancelling as reason for error
            return accountAuthResult;
        }
        catch (FirebaseException exception)
        {
            AuthError authError = (AuthError)exception.ErrorCode; // Get the error code
            accountAuthResult.isFaulted = true; // Set firebase error as reason for error
            accountAuthResult.errorCode = authError; // Pass on the error code
            Debug.Log($"Failed to login account\nRaw Message: {exception.Message}\nError Code: {authError}");
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
