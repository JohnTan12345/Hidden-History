using System.Collections;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameUIScript gameUIScript;
    public ArtifactPageManager artifactPageManager;
    private int totalArtifactsCount = 0;
    private int currentArtifactsCount;
    public int CurrentArtifactsCount {get{return currentArtifactsCount;} set{currentArtifactsCount = value; OnArtifactCountChanged();}}

    void Start()
    {
        StartCoroutine(GetArtifactCollectedCount());
    }
    public void OnArtifactCountChanged()
    {
        gameUIScript.ChangeMissionTrackerText(string.Format("{0}/{1}", currentArtifactsCount, totalArtifactsCount));
    }
    public IEnumerator GetArtifactCollectedCount()
    {
        yield return new WaitUntil(() => Users.DefaultUserLoaded);
        CurrentArtifactsCount = Users.GetDefaultUser().userData.collectedPieces.Count;
        
        foreach(GameObject artifactPage in artifactPageManager.artifactPages)
        {
            Debug.Log(artifactPage);
            Debug.Log(artifactPage.GetComponent<ArtifactPageInfo>());
            Debug.Log(artifactPage.GetComponent<ArtifactPageInfo>().artifacts);
            Debug.Log(artifactPage.GetComponent<ArtifactPageInfo>().artifacts.Count());
            totalArtifactsCount += artifactPage.GetComponent<ArtifactPageInfo>().artifacts.Count();
        };

        OnArtifactCountChanged();
    }
}
