//----------
// Created By: John Tan
// Description: Dig Functions
//----------

using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))] // Always add the button component when this script is added
public class DigButtonFunction : MonoBehaviour
{
    [SerializeField]
    private ImageTracker imageTracker;
    [SerializeField]
    private GameManager gameManager;
    [Header("Progress Bar")]
    [SerializeField]
    [Tooltip("This refers to the Progress Bar entirely")]
    private GameObject DigProgressBarObject;
    [SerializeField]
    [Tooltip("This refers to the bar INSIDE the progress bar")]
    private GameObject DigProgressBar;
    private Button digButton;
    [Header("Found Panel UI")]
    [SerializeField]
    [Tooltip("The button inside the \"found\" panel")]
    private GameObject foundPanel;
    [SerializeField]
    private Button acknowledgeButton;
    
    private GameObject trackedObject;
    private GameObject foundTrackedObject;
    private ArtifactInfo artifactInfo;

    void Start()
    {
        digButton = GetComponent<Button>();
        digButton.onClick.AddListener(OnDig);
        acknowledgeButton.onClick.AddListener(onButtonPressed);
        imageTracker.OnTrackedObjectChanged += OnObjectActive; // Always fire OnObjectActive when tracked object changes
    }

    private void OnObjectActive(GameObject newTrackedObject)
    {
        trackedObject = newTrackedObject;
        artifactInfo = newTrackedObject.GetComponent<ArtifactInfo>();
        UI_Update();
    }
    private void OnDig()
    {
        if (artifactInfo == null) // Check if artifactInfo is null
        {
            OnObjectActive(imageTracker.trackedObject);
        }

        artifactInfo.digProgess++;

        if (artifactInfo.digProgess >= 5) // If player pressed the button 5 times
        {
            Users.GetDefaultUser().userData.collectedPieces.Add(trackedObject.name); // Add artifact to player collected artifacts
            foundTrackedObject = trackedObject;
            gameManager.CurrentArtifactsCount++;
            Users.GetDefaultUser().SaveUserDataAsync();
            Destroy(trackedObject.GetComponent<ArtifactInfo>().dirtMound);
            foundPanel.SetActive(true);
        }
        UI_Update();
    }

    private void onButtonPressed()
    {
        foundPanel.SetActive(false);
        Destroy(foundTrackedObject); // Remove the object
        imageTracker.SetUIActive(false);
    }
    private void UI_Update()
    {
        print(artifactInfo.digProgess / 5f);
        DigProgressBar.transform.localScale = new Vector3(artifactInfo.digProgess/5f, 1, 1);
    }
}
