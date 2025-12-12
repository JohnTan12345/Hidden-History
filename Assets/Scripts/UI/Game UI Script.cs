//-----------------------------------------------------------------------------------------------------------------
// Created By: Rayner Chua
// Description: Game Scene UI Script
//-----------------------------------------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUIScript : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField]
    private GameManager gameManager;
    [Header("Mission UI")]
    [SerializeField]
    private TextMeshProUGUI missionTextUI;
    [Tooltip("What the label is going to look like. Example: \"Mission\" will show up as Mission: [Mission]")]
    public string missionLabel;
    
    [SerializeField]
    private TextMeshProUGUI missionTrackerTextUI;
    [Tooltip("What the label is going to look like. Example: \"Collected\" will show up as Collected: [Amount]")]
    public string missionTrackerLabel;

    [Header("Game Completion UI")]
    [SerializeField]
    private Button restartButton;
    [SerializeField]
    private Button exitButton;

    [Header("Button UI")]
    [SerializeField]
    public List<ButtonRedirectory> buttons = new List<ButtonRedirectory>();
    void Start() // Script Initialization
    {
        StartCoroutine(ScriptInitialization());
    }

    private IEnumerator ScriptInitialization()
    {
        yield return new WaitUntil(() => Users.DefaultUserLoaded); // Yield coroutine to wait for default user to load
        ChangeMissionText(Users.GetDefaultUser().userData.currentMission); // Set mission to current mission

        foreach (ButtonRedirectory buttonRedirectory in buttons) // Add listeners to every button
        {
            buttonRedirectory.button.GetComponent<Button>().onClick.AddListener(() => TogglePage(buttonRedirectory.togglePanels));
        }
    }

    public void TogglePage(GameObject[] togglePanels) // Opening/Closing a panel
    {
        foreach (GameObject togglePanel in togglePanels)
        togglePanel.SetActive(!togglePanel.activeSelf);
    }

    public void ChangeMissionText(string newMission) // Change mission text with new mission
    {
        missionTextUI.text = missionLabel + ": " + newMission;
    }

    public void ChangeMissionTrackerText(string newValueWithMax) // Change mission tracker text with new value with max
    {
        missionTrackerTextUI.text = missionTrackerLabel + ": " + newValueWithMax;
    }

    public async void onUserRestart()
    {
        User user = Users.GetDefaultUser();
        user.userData = new UserData();
        
        await user.SaveUserDataAsync();
        gameManager.artifactsLoaded = false;
        StartCoroutine(gameManager.LoadUserData());
    }
}

[Serializable]
public class ButtonRedirectory
{
    public GameObject button;
    [Tooltip("What the button will toggle")]
    public GameObject[] togglePanels; // The panel that is being opened/closed
}
