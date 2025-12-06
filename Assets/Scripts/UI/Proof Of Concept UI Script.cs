using UnityEngine;

public class ProofOfConceptUIScript : MonoBehaviour
{
    public GameObject notification;
    public GameObject artifactPanel;
    public void onDig()
    {
        notification.SetActive(true);
    }

    public void onAcknowledge()
    {
        notification.SetActive(false);
    }

    public void viewArtifact()
    {
        artifactPanel.SetActive(!artifactPanel.activeSelf);
    }

    public void closeArtifactPanel()
    {
        artifactPanel.SetActive(false);
    }
}
