using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ArtifactPageManager : MonoBehaviour
{
    [Header("Artifact Pages")]
    [Tooltip("Place artifact pages here, the indexes are page numbers")]
    [SerializeField]
    private GameObject[] artifactPages;

    [Header("Artifact Panel")]
    [SerializeField]
    private TextMeshPro artifactTitleText;
    [SerializeField]
    private TextMeshPro artifactDescriptionText;

    [Header("Buttons")]
    [SerializeField]
    private Button openPanelButton;
    [SerializeField]
    private Button closePanelButton;

    private int pageNumber = 0;
    public int PageNumber {get{return pageNumber + 1;}}
    private int totalPages;

    void Start()
    {
        totalPages = artifactPages.Length;

        if (totalPages <= 0)
        {
            throw new System.Exception("You can't have 0 or less artifact pages!\nDid you forget to add in the pages to Artifact Page Manager?");
        }

        openPanelButton.onClick.AddListener(OpenArtifactPanel);
        closePanelButton.onClick.AddListener(CloseArtifactPanel);
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
    private void LoadArtifacts()
    {
        for (int i = 0; i < artifactPages.Length; i++) {
            GameObject artifactPage = artifactPages[i];
            for (int i1 = 0; i1 < artifactPage.GetComponent<ArtifactPageInfo>().artifacts.Length; i1++)
            {
                GameObject artifact = artifactPage.GetComponent<ArtifactPageInfo>().artifacts[i1];
                if (Users.GetDefaultUser().userData.collectedPieces.Contains(artifact.name))
                {
                    artifact.SetActive(true);
                } else
                {
                    artifact.SetActive(false);
                }
            }
        }
    }
    private void OpenArtifactPanel()
    {
        LoadArtifacts();
        gameObject.SetActive(true);
    }

    private void CloseArtifactPanel()
    {
        gameObject.SetActive(false);
    }
}
