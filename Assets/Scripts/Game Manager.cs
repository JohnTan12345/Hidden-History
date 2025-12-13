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
    [SerializeField]
    private ImageTracker imageTracker;
    
    [Header("UI")]
    [SerializeField]
    private GameObject gameCompletedUI;
    private int totalArtifactsCount = 0;
    private int currentArtifactsCount;
    public int CurrentArtifactsCount {get{return currentArtifactsCount;} set{currentArtifactsCount = value; OnArtifactCountChanged();}}

    public bool artifactsLoaded = false;
    private List<GameObject> paintingsList = new List<GameObject>();
    public List<GameObject> completedPaintingsList = new List<GameObject>();
    private List<string> allArtifactsName = new List<string>();

    [Header("SFX")]
    [SerializeField]
    private AudioSource gameFinishSFX;
    private bool SFXPlayed = false;

    void Start()
    {
        if (testing)
        {
            new User().CreateNewUserAsync("TestUser");
        }
        StartCoroutine(LoadUserData());
    }
    void FixedUpdate()
    {
        if (artifactsLoaded && CurrentArtifactsCount >= totalArtifactsCount) // Check if all artifacts are collected
        {
            OnAllArtifactsCollected();
        } else
        {
            gameCompletedUI.SetActive(false);
        }
    }
    public void OnArtifactCountChanged() // When there is a change to current amount
    {
        gameUIScript.ChangeMissionTrackerText($"{currentArtifactsCount}/{totalArtifactsCount}");
    }
    public IEnumerator LoadUserData()
    {
        yield return new WaitUntil(() => Users.DefaultUserLoaded);

        User user = Users.GetDefaultUser();

        LoadArtifactValues(user);
        CheckCollectedArtifactValidity(user);
    }

    private void LoadArtifactValues(User user)
    {
        // Resets all variables

        paintingsList = new List<GameObject>();
        completedPaintingsList = new List<GameObject>();
        artifactsLoaded = false;
        allArtifactsName = new List<string>();
        currentArtifactsCount = 0;
        totalArtifactsCount = 0;
        imageTracker.spawnedObjects = new Dictionary<GameObject, GameObject>();
        imageTracker.spawnedPrefabs = new Dictionary<string, List<GameObject>>();
        SFXPlayed = false;

        List<string> CollectedArtifacts = user.userData.collectedPieces;
        currentArtifactsCount = CollectedArtifacts.Count();

        foreach(GameObject artifactPageObject in artifactPageManager.artifactPages)
        {
            ArtifactPageInfo artifactPageInfo = artifactPageObject.GetComponent<ArtifactPageInfo>();
            Artifact[] artifactList = artifactPageInfo.artifacts;
            totalArtifactsCount += artifactList.Count();
            List<Artifact> remainingArtifacts = new List<Artifact>(); // Creates a list of remaining artifacts to spawn prefabs later

            if (!paintingsList.Contains(artifactPageObject)) // Add the artifact page to list of paintings
            {
                paintingsList.Add(artifactPageObject);
            }
            
            foreach (Artifact artifact in artifactList)
            {
                GameObject artifactObject = artifact.artifact;
                allArtifactsName.Add(artifactObject.name); // Add artifact to the artifacts pool for validity checking later

                if (!user.userData.collectedPieces.Contains(artifact.artifact.name))
                {
                    remainingArtifacts.Add(artifact); // Add artifact to remaining artifacts if user does not have it
                }
            }

            if (remainingArtifacts.Count() == 0 && !completedPaintingsList.Contains(artifactPageObject))
            {
                completedPaintingsList.Add(artifactPageObject); // Add artifact page to completed paintings
            }

            artifactPageInfo.LoadArtifacts(); // Load artifacts inside
            
            bool lastArtifactPage = false;
            if (artifactPageObject == artifactPageManager.artifactPages.Last())
            {
                lastArtifactPage = true; // Tell the imageTracker SetupPrefab that this is the last artifact page
            }
            imageTracker.SetupPrefab(remainingArtifacts, artifactPageInfo.trackedImage.name, lastArtifactPage); // Spawn artifacts
        }

        OnArtifactCountChanged();
        artifactsLoaded = true;
    }

    private void CheckCollectedArtifactValidity(User user) // Check if the artifact even exists in the game
    {
        foreach (string collectedArtifactName in user.userData.collectedPieces)
        {
            if (!allArtifactsName.Contains(collectedArtifactName))
            {
                user.userData.collectedPieces.Remove(collectedArtifactName);
            }
        }
    }

    private void OnAllArtifactsCollected()
    {
        if (gameCompletedUI.activeSelf == false && !SFXPlayed) // Play SFX and enable the game ending scene 
        {
            gameFinishSFX.Play();
            gameCompletedUI.SetActive(true);

            SFXPlayed = true; // Make sure the sfx does not play again if the player restarts
        }
    }
}
