
using System;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Authentication.PlayerAccounts;
using Unity.Services.Core;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LoginController : MonoBehaviour
{
    [SerializeField] private Button loginButton;
    [SerializeField] private Button signUpButton;
    [SerializeField] private TMP_InputField usernameInput;
    [SerializeField] private TMP_InputField passwordInput;
    [SerializeField] private TMP_Text errorMessageText;

    public bool isSignedIn;

    private async void Awake()
    {
        await UnityServices.InitializeAsync();
        bool isSignedIn = AuthenticationService.Instance.IsSignedIn;
    }

    public async void SignUp(){
        string username = usernameInput.text;
        string password = passwordInput.text;
        await SignUpWithUsernamePasswordAsync(username, password);
    }

    public async void SignIn(){
        string username = usernameInput.text;
        string password = passwordInput.text;
        await SignInWithUsernamePasswordAsync(username, password);
    }

    public async void SignOut(){
        await signOutAccount();
    }

    async Task SignUpWithUsernamePasswordAsync(string username, string password)
    {
        try
        {
            await AuthenticationService.Instance.SignUpWithUsernamePasswordAsync(username, password);
            Debug.Log("SignUp is successful.");
        }
        catch (AuthenticationException ex)
        {
            ShowErrorMessage(ex.Message);
            // Compare error code to AuthenticationErrorCodes
            // Notify the player with the proper error message
            Debug.LogException(ex);
        }
        catch (RequestFailedException ex)
        {
            ShowErrorMessage(ex.Message);
            // Compare error code to CommonErrorCodes
            // Notify the player with the proper error message
            Debug.LogException(ex);
        }
    }

    async Task SignInWithUsernamePasswordAsync(string username, string password){
       try
        {
            await AuthenticationService.Instance.SignInWithUsernamePasswordAsync(username, password);
            isSignedIn = true;
            Debug.Log("Sign In successful.");
        }
        catch (AuthenticationException ex)
        {
            ShowErrorMessage(ex.Message);
            // Compare error code to AuthenticationErrorCodes
            // Notify the player with the proper error message
            Debug.LogException(ex);
        }
        catch (RequestFailedException ex)
        {
            ShowErrorMessage(ex.Message);
            // Compare error code to CommonErrorCodes
            // Notify the player with the proper error message
            Debug.LogException(ex);
        } 
    }

    async Task signOutAccount(){
        try
        {
            AuthenticationService.Instance.SignOut(true);
            isSignedIn = false;
            Debug.Log("Sign In successful.");
        }
        catch (AuthenticationException ex)
        {
            ShowErrorMessage(ex.Message);
            // Compare error code to AuthenticationErrorCodes
            // Notify the player with the proper error message
            Debug.LogException(ex);
        }
        catch (RequestFailedException ex)
        {
            ShowErrorMessage(ex.Message);
            // Compare error code to CommonErrorCodes
            // Notify the player with the proper error message
            Debug.LogException(ex);
        } 
    }
    
    public void ShowErrorMessage(string message){
        errorMessageText.text = message;
        errorMessageText.gameObject.SetActive(true);
    }

    public void HideErrorMessage(){
        errorMessageText.gameObject.SetActive(false);
    }

    
}

