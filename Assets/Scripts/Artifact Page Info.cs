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
}