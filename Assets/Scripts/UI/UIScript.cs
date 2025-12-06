using System.Collections;
using System.Collections.Generic;
using Firebase.Database;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIScript : MonoBehaviour
{
    public GameObject loginButton;
    public GameObject changeLoginSignUpButton;
    public GameObject emailInput;
    public GameObject passwordInput;

    private string email;
    private string password;
    private bool isLogin = true;

    void Start()
    {
        emailInput.GetComponent<TMP_InputField>().onEndEdit.AddListener(ChangeEmail);
        passwordInput.GetComponent<TMP_InputField>().onEndEdit.AddListener(ChangePassword);
    }

    private void ChangeEmail(string afterEdit)
    {
        email = afterEdit;
    }

    public void ChangePassword(string afterEdit)
    {
        password = afterEdit;
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
            Login();
        }
        else
        {
            SignUp();
        }
    }

    private async void Login()
    {
        AccountAuthResult accountAuthResult = await Auth.LoginAccountAsync(email, password);

        AuthResultHandler(accountAuthResult);
    }

    private async void SignUp()
    {
        AccountAuthResult accountAuthResult = await Auth.CreateNewAccountAsync(email, password);

        AuthResultHandler(accountAuthResult);
    }

    private async void AuthResultHandler(AccountAuthResult accountAuthResult)
    {
        if (accountAuthResult.isFaulted)
        {
            Debug.Log(accountAuthResult.errorCode);
        } else if (accountAuthResult.isCancelled)
        {
            Debug.Log("Process cancelled");
        } else
        {
            await new User().CreateNewUserAsync(accountAuthResult.userID);
            StartCoroutine(LoadGame());
        }
    }

    private IEnumerator LoadGame()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(1);

        while(!asyncLoad.isDone)
        {
            yield return null;
        }
    }
    
}
