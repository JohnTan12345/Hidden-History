//-----------------------------------------------------------------------------------------------------------------
// Created By: John Tan
// Description: Firebase Error Code Handler
//-----------------------------------------------------------------------------------------------------------------

using Firebase.Auth;

public static class DatabaseErrorHandler
{
    public static string LoginErrorMessage(AuthError authError)
    {
        string returnString = $"There was an error logging you in, Error Code: {authError}";

        switch(authError)
        {
        case AuthError.InvalidCredential:
            returnString = "You cannot use this provider";
            break;
        case AuthError.UserDisabled:
            returnString = "Your account has been disabled";
            break;
        case AuthError.AccountExistsWithDifferentCredentials:
            returnString = "There exists an account with the same email using a different provider";
            break; 
        case AuthError.EmailAlreadyInUse:
            returnString = "There exists an account with the same email";
            break;
        case AuthError.CredentialAlreadyInUse:
            returnString = "The account already exists, please login";
            break;
        case AuthError.InvalidEmail:
            returnString = "Please enter a valid email address";
            break;
        case AuthError.WrongPassword:
            returnString = "The password is incorrect";
            break;
        case AuthError.TooManyRequests:
            returnString = "Too many login attempts! Try again later";
            break;
        case AuthError.UserNotFound:
            returnString = "Account does not exist, please sign up";
            break;
        case AuthError.WeakPassword:
            returnString = "Password is too weak, Password needs to be at least 6 characters long";
            break;
        case AuthError.MissingEmail:
            returnString = "Please enter an email";
            break;
        case AuthError.MissingPassword:
            returnString = "Please enter a password";
            break;
        case AuthError.UnverifiedEmail:
            returnString = "Please enter a valid email address";
            break;
        }

        return returnString;
    }
}