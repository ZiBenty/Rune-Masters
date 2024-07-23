using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using Unity.Services.Authentication;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class UserController : MonoBehaviour
{
    [SerializeField] private TMP_Text usernameText;
    [SerializeField] private GameObject userPanel;
    [SerializeField] private GameObject LoginPanel;
    [SerializeField] private Button AuthButton;
    [SerializeField] private Button UserButton;
    public bool loginPanel;

    void Start(){
        loginPanel = false;
        AuthenticationService.Instance.SignedIn += SignIn;
        AuthenticationService.Instance.SignedIn += AuthPanel;
        AuthenticationService.Instance.SignedOut += SignOut;
    }
    
    public void AuthPanel(){
        loginPanel = !loginPanel;
        LoginPanel.SetActive(loginPanel);
        userPanel.SetActive(!loginPanel);
    }

    public async void SignIn(){
        UserButton.gameObject.SetActive(true);
        AuthButton.gameObject.SetActive(false);
        await UpdateNameText(AuthenticationService.Instance.PlayerName);
    }

    public void SignOut(){
        UserButton.gameObject.SetActive(false);
        AuthButton.gameObject.SetActive(true);
        UpdateNameText("");
    }

    public Task UpdateNameText(string name){
        usernameText.text = name;
        return Task.CompletedTask;
    }

    void OnDestroy(){
        AuthenticationService.Instance.SignedIn -= SignIn;
        AuthenticationService.Instance.SignedIn -= AuthPanel;
        AuthenticationService.Instance.SignedOut -= SignOut;
    }
}
