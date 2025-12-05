using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class DigButtonFunction : MonoBehaviour
{
    [SerializeField]
    private ImageTracker imageTracker;
    [Header("Progress Bar")]
    [SerializeField]
    [Tooltip("This refers to the Progress Bar entirely")]
    private GameObject DigProgressBarObject;
    [SerializeField]
    [Tooltip("This refers to the bar INSIDE the progress bar")]
    private GameObject DigProgressBar;
    private Button button;
    
    private GameObject trackedObject;
    private ArtifactInfo artifactInfo;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnDig);
        imageTracker.OnTrackedObjectChanged += OnObjectActive;
    }

    private void OnObjectActive(GameObject newTrackedObject)
    {
        trackedObject = newTrackedObject;
        artifactInfo = newTrackedObject.GetComponent<ArtifactInfo>();
        UI_Update();
    }
    private void OnDig()
    {
        if (artifactInfo == null)
        {
            OnObjectActive(imageTracker.trackedObject);
        }

        artifactInfo.digProgess += 1;
        if (artifactInfo.digProgess >= 5)
        {
            Destroy(trackedObject);
            imageTracker.SetUIActive(false);
        }
        UI_Update();
    }

    private void UI_Update()
    {
        print(artifactInfo.digProgess / 5f);
        DigProgressBar.transform.localScale = new Vector3(artifactInfo.digProgess/5f, 1, 1);
    }
}
