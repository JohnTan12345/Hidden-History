//----------
// Edited by: John Tan
// Description: Added a way to toggle the dig button as well as only load needed prefabs
//----------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;


public class ImageTracker : MonoBehaviour
{
    public GameObject DigButton;
    public GameObject ProgressBar;
    public GameObject trackedObject;
    public event Action<GameObject> OnTrackedObjectChanged;
    [SerializeField]
    private ARTrackedImageManager trackedImageManager;

    [SerializeField]
    private GameObject[] placeablePrefabs;

    private bool PrefabsLoaded = false;

    private Dictionary<string, GameObject> spawnedPrefabs = new Dictionary<string, GameObject>();

    private Dictionary<GameObject, GameObject> spawnedObjects = new Dictionary<GameObject, GameObject>();

    private string[] someArray = new string[]{"Image1", "Image2", "Image3"};

    private void Start()
    {
        if (trackedImageManager != null)
        {
            trackedImageManager.trackablesChanged.AddListener(OnImageChanged);
            StartCoroutine(SetupPrefabs());
        }
    }

    IEnumerator SetupPrefabs()
    {
        yield return new WaitUntil(() => Users.DefaultUserLoaded && Users.GetDefaultUser().DataLoaded);
        Debug.Log("User Found!");
        foreach (GameObject prefab in placeablePrefabs)
        {
            if (!Users.GetDefaultUser().userData.collectedPieces.Contains(prefab.name))
            {
                GameObject newPrefab = Instantiate(prefab);
                newPrefab.name = prefab.name;
                newPrefab.SetActive(false);
                spawnedPrefabs.Add(prefab.name, newPrefab);
                spawnedObjects.Add(newPrefab, prefab);
            }
        }
        PrefabsLoaded = true;
    }

    void OnImageChanged(ARTrackablesChangedEventArgs<ARTrackedImage> eventArgs)
    {
        foreach (ARTrackedImage trackedImage in eventArgs.added)
        {
            UpdateImage(trackedImage);
        }

        foreach (ARTrackedImage trackedImage in eventArgs.updated)
        {
            UpdateImage(trackedImage);
        }

        foreach (KeyValuePair<TrackableId, ARTrackedImage> lostObj in eventArgs.removed)
        {
            UpdateImage(lostObj.Value);
        }
    }

    void UpdateImage(ARTrackedImage trackedImage)
    {
        if(trackedImage != null && PrefabsLoaded)
        {
            if (trackedImage.trackingState == TrackingState.Limited || trackedImage.trackingState == TrackingState.None)
            {
                //Disable the associated content
                try {
                    spawnedPrefabs[trackedImage.referenceImage.name].transform.SetParent(null);
                    spawnedPrefabs[trackedImage.referenceImage.name].SetActive(false);
                } catch (MissingReferenceException)
                {
                    print("Object has been deleted");
                }
                SetUIActive(false);
                trackedObject = null;
            }
            else if (trackedImage.trackingState == TrackingState.Tracking)
            {
                //Enable the associated content

                // Make the dig button visible
                try {
                    if(spawnedPrefabs[trackedImage.referenceImage.name].transform.parent != trackedImage.transform)
                    {
                        Debug.Log("Enabling associated content: " + spawnedPrefabs[trackedImage.referenceImage.name].name);
                        spawnedPrefabs[trackedImage.referenceImage.name].transform.SetParent(trackedImage.transform);
                        spawnedPrefabs[trackedImage.referenceImage.name].transform.localPosition = spawnedObjects[spawnedPrefabs[trackedImage.referenceImage.name]].transform.localPosition;
                        spawnedPrefabs[trackedImage.referenceImage.name].transform.localRotation = spawnedObjects[spawnedPrefabs[trackedImage.referenceImage.name]].transform.localRotation;

                        spawnedPrefabs[trackedImage.referenceImage.name].SetActive(true);
                        trackedObject = spawnedPrefabs[trackedImage.referenceImage.name];
                        OnTrackedObjectChanged?.Invoke(trackedObject);
                        SetUIActive(true);
                    }
                } catch (MissingReferenceException)
                {
                    trackedObject = null;
                    SetUIActive(false);
                } catch (KeyNotFoundException)
                {
                    Debug.Log("Object either has bad naming convention, or is already found");
                }
            }
        }
    }

    public void SetUIActive(bool value)
    {
        DigButton.SetActive(value);
        ProgressBar.SetActive(value);
    }
}
