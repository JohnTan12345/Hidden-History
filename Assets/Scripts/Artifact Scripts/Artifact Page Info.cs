using System.Collections;
using UnityEngine;
[System.Serializable]
public class ArtifactPageInfo : MonoBehaviour
{
    [Header("Artifact Information")]
    public string title;
    public string description;
    public GameObject silhouette;

    [Header("Artifact Images")]
    public GameObject[] artifacts;

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
                    artifact.SetActive(true);
                }
                else
                {
                    artifact.SetActive(false);
                }
            }
        }
    }
}