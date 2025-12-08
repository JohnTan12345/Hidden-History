using System.Collections;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Testing Parameters")]
    public bool testing;
    [Header("Normal Parameters")]
    public GameUIScript gameUIScript;
    public ArtifactPageManager artifactPageManager;
    private int totalArtifactsCount = 0;
    private int currentArtifactsCount;
    public int CurrentArtifactsCount {get{return currentArtifactsCount;} set{currentArtifactsCount = value; OnArtifactCountChanged();}}

    void Start()
    {
        if (testing)
        {
            new User().CreateNewUserAsync("TestUser");
        }
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
            totalArtifactsCount += artifactPage.GetComponent<ArtifactPageInfo>().artifacts.Count();
        };

        OnArtifactCountChanged();
    }
}
