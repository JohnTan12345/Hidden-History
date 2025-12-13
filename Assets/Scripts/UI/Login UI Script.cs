//-----------------------------------------------------------------------------------------------------------------
// Created By: John Tan
// Description: Login Scene UI Script
//-----------------------------------------------------------------------------------------------------------------

using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginUIScript : MonoBehaviour
{
    // UI
    [Header("UI")]
    [SerializeField]
    private GameObject loginButton;
    [SerializeField]
    private GameObject changeLoginSignUpButton;
    [SerializeField]
    private TMP_InputField emailInput;
    [SerializeField]
    private TMP_InputField passwordInput;
    [SerializeField]
    private TextMeshProUGUI errorMessage;
    
    // Script Variables
    private string email;
    private string password;
    private bool isLogin = true;
    

    void Start()
    {
        // Add listeners to the buttons/text input
        emailInput.onEndEdit.AddListener(ChangeEmail);
        passwordInput.onEndEdit.AddListener(ChangePassword);
        loginButton.GetComponent<Button>().onClick.AddListener(LoginSignUpAccount);
        changeLoginSignUpButton.GetComponent<Button>().onClick.AddListener(SwitchLoginSignUp);
    }

    private void ChangeEmail(string afterEdit) // Change email address after editing text input
    {
        email = afterEdit;
    }

    public void ChangePassword(string afterEdit) // Change password after editing text input
    {
        password = afterEdit;
    }

    private void SwitchLoginSignUp() // Switch to login/sign up
    {
        // Get text from button
        TMP_Text loginButtonText = loginButton.transform.GetChild(0).GetComponent<TMP_Text>();
        TMP_Text changeLoginSignUpText = changeLoginSignUpButton.transform.GetChild(0).GetComponent<TMP_Text>();

        isLogin = !isLogin;

        errorMessage.gameObject.SetActive(false); // Hide error message

        // Change text to accurately reflect login/sign up
        if (isLogin)
        {
            loginButtonText.text = "Login";
            changeLoginSignUpText.text = "Don't have an account? Sign up here!";
        }
        else
        {
            loginButtonText.text = "Sign Up";
            changeLoginSignUpText.text = "Have an Account? Login here!";
        }
    }

    private void LoginSignUpAccount() // When the login/sign up button is clicked
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
        AccountAuthResult accountAuthResult = await Auth.LoginAccountAsync(email, password); // Wait for firebase

        AuthResultHandler(accountAuthResult);
    }

    private async void SignUp()
    {
        AccountAuthResult accountAuthResult = await Auth.CreateNewAccountAsync(email, password); // Wait for firebase

        AuthResultHandler(accountAuthResult);
    }

    private async void AuthResultHandler(AccountAuthResult accountAuthResult)
    {
        if (accountAuthResult.isFaulted) // If there was an error due to firebase
        {
            errorMessage.text = DatabaseErrorHandler.LoginErrorMessage(accountAuthResult.errorCode); // Get the text from Database Error Handler
            errorMessage.gameObject.SetActive(true); // Make error message visible
        }
        else if (accountAuthResult.isCancelled) // If there was an error due to task cancelling
        {
            Debug.Log("Process cancelled");
        }
        else // If no errors at all
        {
            if (errorMessage.gameObject.activeSelf) // Disable the error message if it's active
            {
                errorMessage.gameObject.SetActive(false);
            }

            await new User().CreateNewUserAsync(accountAuthResult.userID); // wait for new user to be created
            StartCoroutine(LoadGame()); // Load game after everything is done
        }
    }

    private IEnumerator LoadGame()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(1); // Used async for it to wait for all async functions to finish

        while(!asyncLoad.isDone) // Wait for scene to load
        {
            yield return null;
        }
    }
    
}
