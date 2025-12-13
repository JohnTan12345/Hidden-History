//-----------------------------------------------------------------------------------------------------------------
// Created By: Rayner Chua
// Description: Artifact Panel Functions
//-----------------------------------------------------------------------------------------------------------------

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
    [SerializeField]
    private GameObject paintingFrame;

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

        print(totalPages);

        if (totalPages <= 0) // Check if there is artifact pages
        {
            throw new System.Exception("You can't have 0 or less artifact pages!\nDid you forget to add in the pages to Artifact Page Manager?");
        }

        for (int i = 0; i < artifactPages.Length; i++) // Disable all artifact pages
        {
            GameObject artifactPage = artifactPages[i];
            if (artifactPage != artifactPages[pageNumber])
            {
                artifactPage.SetActive(false);
            }
            Debug.Log(paintingFrame);
            artifactPage.GetComponent<ArtifactPageInfo>().paintingFrame = paintingFrame;
        }

        // Append functions to buttons
        nextPageButton.onClick.AddListener(NextPage);
        prevPageButton.onClick.AddListener(PreviousPage);
    }
    void OnEnable()
    {
        GetPageInfo(); // Enable current artifact page
    }
    void OnDisable()
    {
        artifactPages[pageNumber].SetActive(false); // Disable current artifact page
    }
    private void NextPage()
    {
        artifactPages[pageNumber].SetActive(false); // Disable current artifact page
        pageNumber = pageNumber<totalPages?pageNumber+1:0; // Increase counter
        GetPageInfo();
    }

    private void PreviousPage()
    {
        artifactPages[pageNumber].SetActive(false); // Disable current artifact page
        pageNumber = pageNumber>0?pageNumber-1:totalPages; // Increase counter
        GetPageInfo();
    }
    private void GetPageInfo() // Load page
    {
        GameObject artifactPage = artifactPages[pageNumber]; // Get new artifact page
        ArtifactPageInfo artifactPageInfo = artifactPage.GetComponent<ArtifactPageInfo>();
        artifactTitleText.text = artifactPageInfo.title; // Set painting title
        artifactDescriptionText.text = artifactPageInfo.description; // Set painting description
        artifactPage.SetActive(true); // Enable page
    }
}
