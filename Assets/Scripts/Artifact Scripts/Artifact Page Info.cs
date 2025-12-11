//-----------------------------------------------------------------------------------------------------------------
// Created By: Rayner Chua
// Description: Artiface Page
//-----------------------------------------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
[Serializable]
public class ArtifactPageInfo : MonoBehaviour
{
    [Header("Artifact Information")]
    public string title;
    public string description;
    public GameObject silhouette;
    public Transform painting;
    public Texture2D trackedImage;

    [Header("Artifact Images")]
    [Tooltip("This refers to the gameobject that contains all the artifacts")]
    public GameObject artifactGroupGameObject;
    public Artifact[] artifacts;
    private List<GameObject> collectedArtifacts;
    [Tooltip("The painting that is shown after collecting all artifacts")]
    [HideInInspector]
    private int collected = 0;
    
    [HideInInspector]
    public bool artifactsLoaded = false;
    void OnEnable()
    {
        StartCoroutine(LoadArtifacts());
    }

    public IEnumerator LoadArtifacts()
    {
        collectedArtifacts = new List<GameObject>();
        yield return new WaitUntil(() => Users.DefaultUserLoaded); // Wait for default user to load
        User user = Users.GetDefaultUser();
        foreach (Artifact artifact in artifacts)
        {
            Debug.Log("searching");
            if (user.userData.collectedPieces.Contains(artifact.artifact.name)) // If user collected the artifact already
            {
                Debug.Log("hit");
                collectedArtifacts.Add(artifact.artifact);
                artifact.artifact.SetActive(true);
                collected++;
            }
            else
            {
                artifact.artifact.SetActive(false);
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

[Serializable]
public class Artifact
{
    public GameObject artifact;
    public GameObject artifactPrefab;
}