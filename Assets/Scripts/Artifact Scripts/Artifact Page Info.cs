//-----------------------------------------------------------------------------------------------------------------
// Created By: Rayner Chua
// Description: Artiface Page
//-----------------------------------------------------------------------------------------------------------------

using System.Collections;
using System.Linq;
using UnityEngine;
[System.Serializable]
public class ArtifactPageInfo : MonoBehaviour
{
    [Header("Artifact Information")]
    public string title;
    public string description;
    public GameObject silhouette;
    public Transform painting;

    [Header("Artifact Images")]
    [Tooltip("This refers to the gameobject that contains all the artifacts")]
    public GameObject artifactGroupGameObject;
    public GameObject[] artifacts;
    private GameObject[] collectedArtifacts;
    [Tooltip("The painting that is shown after collecting all artifacts")]
    [HideInInspector]
    public GameObject[] remainingArtifacts; // Future use
    private int collected = 0;
    void OnEnable()
    {
        StartCoroutine(LoadArtifacts());
    }

    public IEnumerator LoadArtifacts()
    {
        yield return new WaitUntil(() => Users.DefaultUserLoaded); // Wait for default user to load
        User user = Users.GetDefaultUser();
        foreach (string collectedArtifact in user.userData.collectedPieces)
        {
            foreach (GameObject artifact in artifacts)
            {
                if (collectedArtifact == artifact.name) // If user collected the artifact already
                {
                    collectedArtifacts.Append(artifact);
                    collected++;
                }
                else
                {
                    remainingArtifacts.Append(artifact);
                }
            }
        }

        if (collected == artifacts.Length)
        {
            onAllArtifactCollected();
        }
    }

    private void onAllArtifactCollected() // Enable the painting and disable the artifacts
    {
        artifactGroupGameObject.SetActive(false);
        silhouette.SetActive(false);
        painting.GetChild(0).gameObject.SetActive(true);
    }
}