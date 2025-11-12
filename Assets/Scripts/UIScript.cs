using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIScript : MonoBehaviour
{
    public GameObject loginButton;
    public GameObject changeLoginSignUpButton;
    public GameObject emailInput;
    public GameObject passwordInput;

    private string email;
    private string password;
    private bool isLogin = true;
    private Coroutine loadDataProcess = null;

    public void ChangeEmail()
    {
        email = emailInput.GetComponent<TMP_InputField>().text;
    }

    public void ChangePassword()
    {
        password = passwordInput.GetComponent<TMP_InputField>().text;
    }

    public void SwitchLoginSignUp()
    {
        TMP_Text loginButtonText = loginButton.transform.GetChild(0).GetComponent<TMP_Text>();
        TMP_Text changeLoginSignUpText = changeLoginSignUpButton.transform.GetChild(0).GetComponent<TMP_Text>();
        isLogin = !isLogin;

        if (isLogin)
        {
            loginButtonText.text = "Login";
            changeLoginSignUpText.text = "No Account? Click here";
        }
        else
        {
            loginButtonText.text = "Sign Up";
            changeLoginSignUpText.text = "Have an Account? Click here";
        }
    }

    public void LoginSignUpAccount()
    {
        if (isLogin)
        {
            StartCoroutine(Login());
        }
        else
        {
            StartCoroutine(SignUp());
        }
    }

    private IEnumerator Login()
    {
        DatabaseAuthInfo databaseAuthInfo = DatabaseAuth.LoginUserEmailPasswordAsync(email, password);
        yield return new WaitUntil(() => databaseAuthInfo.isCompleted);

        Debug.Log("Login Result: " + databaseAuthInfo.info);
    }

    private IEnumerator SignUp()
    {
        DatabaseAuthInfo databaseAuthInfo = DatabaseAuth.CreateUserEmailPasswordAsync(email, password);
        yield return new WaitUntil(() => databaseAuthInfo.isCompleted);

        Debug.Log("Signup Result: " + databaseAuthInfo.info);
    }
}

// Placeholder classes to prevent compile errors
public class DatabaseAuthInfo
{
    public bool isCompleted = true;
    public string info = "Simulated response";
}

public static class DatabaseAuth
{
    public static DatabaseAuthInfo LoginUserEmailPasswordAsync(string email, string password)
    {
        return new DatabaseAuthInfo { info = $"Logged in as {email}" };
    }

    public static DatabaseAuthInfo CreateUserEmailPasswordAsync(string email, string password)
    {
        return new DatabaseAuthInfo { info = $"Signed up as {email}" };
    }
}
