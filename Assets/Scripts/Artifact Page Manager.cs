using TMPro;
using UnityEngine;

public class ArtifactPageManager : MonoBehaviour
{
    [Header("Artifact Pages")]
    [Tooltip("Place artifact pages here, the indexes are page numbers")]
    public GameObject[] artifactPages;

    [Header("Artifact Panel")]
    public GameObject artifactPanel;
    public TextMeshProUGUI artifactTitleText;
    public TextMeshProUGUI artifactDescriptionText;

    private int pageNumber = 0;
    public int PageNumber {get{return pageNumber + 1;}}
    private int totalPages;

    void Start()
    {
        totalPages = artifactPages.Length;

        if (totalPages <= 0)
        {
            throw new System.Exception("You can't have 0 or less artifact pages! Did you forget to add in the pages to Artifact Page Manager?");
        }
    }
    public void NextPage()
    {
        artifactPages[pageNumber].SetActive(false);
        pageNumber = pageNumber<totalPages?pageNumber++:0;
        GetPageInfo();
    }

    public void PreviousPage()
    {
        artifactPages[pageNumber].SetActive(false);
        pageNumber = pageNumber>0?pageNumber--:totalPages;
        GetPageInfo();
    }
    private void GetPageInfo()
    {
        GameObject artifactPage = artifactPages[pageNumber];
        ArtifactPageInfo artifactPageInfo = artifactPage.GetComponent<ArtifactPageInfo>();
        artifactTitleText.text = artifactPageInfo.title;
        artifactDescriptionText.text = artifactPageInfo.description;
        artifactPage.SetActive(true);
    }

    public void ToggleArtifactPanel()
    {
        bool isActive = artifactPanel.activeSelf;
        artifactPanel.SetActive(!isActive);

        if (!isActive)
        {
            GetPageInfo();
        }
    }
}
