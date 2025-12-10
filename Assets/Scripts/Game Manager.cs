//-----------------------------------------------------------------------------------------------------------------
// Created By: Rayner Chua
// Description: Load data from user data to in-game UI
//-----------------------------------------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Testing Parameters")]
    public bool testing;
    [Header("Scripts")]
    [SerializeField]
    private GameUIScript gameUIScript;
    [SerializeField]
    private ArtifactPageManager artifactPageManager;
    private int totalArtifactsCount = 0;
    private int currentArtifactsCount;
    public int CurrentArtifactsCount {get{return currentArtifactsCount;} set{currentArtifactsCount = value; OnArtifactCountChanged();}}

    void Start()
    {
        if (testing)
        {
            new User().CreateNewUserAsync("TestUser");
        }
        StartCoroutine(LoadUserData());
    }
    public void OnArtifactCountChanged() // When there is a change to current amount
    {
        gameUIScript.ChangeMissionTrackerText($"{currentArtifactsCount}/{totalArtifactsCount}");
    }
    private IEnumerator LoadUserData()
    {
        yield return new WaitUntil(() => Users.DefaultUserLoaded);

        User user = Users.GetDefaultUser();
        LoadArtifactValues(user);
        
    }

    private void LoadArtifactValues(User user)
    {
        List<string> CollectedArtifacts = user.userData.collectedPieces;
        currentArtifactsCount = CollectedArtifacts.Count();

        foreach(GameObject artifactPageObject in artifactPageManager.artifactPages)
        {
            ArtifactPageInfo artifactPageInfo = artifactPageObject.GetComponent<ArtifactPageInfo>();
            totalArtifactsCount += artifactPageInfo.artifacts.Count();

            StartCoroutine(artifactPageInfo.LoadArtifacts());
        }

        OnArtifactCountChanged();
    }
}
