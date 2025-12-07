using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ArtifactPageManager : MonoBehaviour
{
    public GameManager gameManager;
    [Header("Artifact Pages")]
    [Tooltip("Place artifact pages here, the indexes are page numbers")]
    [SerializeField]
    public GameObject[] artifactPages;

    [Header("Artifact Panel")]
    [SerializeField]
    private TextMeshProUGUI artifactTitleText;
    [SerializeField]
    private TextMeshProUGUI artifactDescriptionText;

    [Header("Buttons")]
    [SerializeField]
    private Button nextPageButton;
    [SerializeField]
    private Button prevPageButton;

    private int pageNumber = 0;
    public int PageNumber {get{return pageNumber + 1;}}
    private int totalPages;

    void Start()
    {
        totalPages = artifactPages.Length - 1;

        if (totalPages <= 0)
        {
            throw new System.Exception("You can't have 0 or less artifact pages!\nDid you forget to add in the pages to Artifact Page Manager?");
        }

        foreach (GameObject artifactPage in artifactPages)
        {
            artifactPage.SetActive(false);
        }

        nextPageButton.onClick.AddListener(NextPage);
        prevPageButton.onClick.AddListener(PreviousPage);
    }
    void OnEnable()
    {
        artifactPages[pageNumber].SetActive(true);
    }
    void OnDisable()
    {
        artifactPages[pageNumber].SetActive(false);
    }
    public void NextPage()
    {
        artifactPages[pageNumber].SetActive(false);
        pageNumber = pageNumber<totalPages?pageNumber+1:0;
        GetPageInfo();
    }

    public void PreviousPage()
    {
        artifactPages[pageNumber].SetActive(false);
        pageNumber = pageNumber>0?pageNumber-1:totalPages;
        GetPageInfo();
    }
    private void GetPageInfo()
    {
        GameObject artifactPage = artifactPages[pageNumber];
        ArtifactPageInfo artifactPageInfo = artifactPage.GetComponent<ArtifactPageInfo>();
        artifactTitleText.text = artifactPageInfo.title;
        artifactDescriptionText.text = artifactPageInfo.description;
        Debug.Log(artifactPage);
        artifactPage.SetActive(true);
    }
}
