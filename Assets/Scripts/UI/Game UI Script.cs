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
    
    public void Redirect(GameObject destinationPanel)
    {
        destinationPanel.SetActive(!destinationPanel.activeSelf);
    }

    void Start() // Script Initialization
    {
        StartCoroutine(ScriptInitialization());
    }

    private IEnumerator ScriptInitialization()
    {
        yield return new WaitUntil(() => Users.DefaultUserLoaded);
        ChangeMissionText(Users.GetDefaultUser().userData.currentMission);
        foreach (ButtonRedirectory buttonRedirectory in buttons)
        {
            buttonRedirectory.button.GetComponent<Button>().onClick.AddListener(() => Redirect(buttonRedirectory.destinationPanel));
        }
    }
    public void ChangeMissionText(string newMission)
    {
        missionTextUI.text = missionLabel + ": " + newMission;
    }

    public void ChangeMissionTrackerText(string newValue)
    {
        missionTrackerTextUI.text = missionTrackerLabel + ": " + newValue;
    }
}

[Serializable]
public class ButtonRedirectory
{
    public GameObject button;
    [Tooltip("What the button will toggle")]
    public GameObject destinationPanel;
}
