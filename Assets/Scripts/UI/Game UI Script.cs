//-----------------------------------------------------------------------------------------------------------------
// Created By: Rayner Chua
// Description: Game Scene UI Script
//-----------------------------------------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUIScript : MonoBehaviour
{
    [Header("Mission UI")]
    [SerializeField]
    private TextMeshProUGUI missionTextUI;
    [Tooltip("What the label is going to look like. Example: \"Mission\" will show up as Mission: [Mission]")]
    public string missionLabel;
    
    [SerializeField]
    private TextMeshProUGUI missionTrackerTextUI;
    [Tooltip("What the label is going to look like. Example: \"Collected\" will show up as Collected: [Amount]")]
    public string missionTrackerLabel;

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
            buttonRedirectory.button.GetComponent<Button>().onClick.AddListener(() => Redirect(buttonRedirectory.destinationPanel));
        }
    }

    public void Redirect(GameObject destinationPanel) // Opening/Closing a panel
    {
        destinationPanel.SetActive(!destinationPanel.activeSelf);
    }

    public void ChangeMissionText(string newMission) // Change mission text with new mission
    {
        missionTextUI.text = missionLabel + ": " + newMission;
    }

    public void ChangeMissionTrackerText(string newValueWithMax) // Change mission tracker text with new value with max
    {
        missionTrackerTextUI.text = missionTrackerLabel + ": " + newValueWithMax;
    }
}

[Serializable]
public class ButtonRedirectory
{
    public GameObject button;
    [Tooltip("What the button will toggle")]
    public GameObject destinationPanel; // The panel that is being opened/closed
}
