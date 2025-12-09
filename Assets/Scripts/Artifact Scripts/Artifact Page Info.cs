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

    [Header("Artifact Images")]
    [Tooltip("This refers to the gameobject that contains all the artifacts")]
    public GameObject artifactGroupGameObject;
    public GameObject[] artifacts;
    private GameObject[] collectedArtifacts;
    [Tooltip("The painting that is shown after collecting all artifacts")]
    [HideInInspector]
    public GameObject[] remainingArtifacts; // Future use
    public GameObject fullPainting;
    private int collected = 0;
    void OnEnable()
    {
        StartCoroutine(LoadArtifacts());
    }

    private IEnumerator LoadArtifacts()
    {
        yield return new WaitUntil(() => Users.DefaultUserLoaded);
        User user = Users.GetDefaultUser();
        foreach (string collectedArtifact in user.userData.collectedPieces)
        {
            foreach (GameObject artifact in artifacts)
            {
                if (collectedArtifact == artifact.name)
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

    private void onAllArtifactCollected()
    {
        artifactGroupGameObject.SetActive(false);
        silhouette.SetActive(false);
        fullPainting.SetActive(true);
    }
}