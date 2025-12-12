//-----------------------------------------------------------------------------------------------------------------
// Edited By: John Tan
// Description: Added in ways for the script to automatically add in onother prefab
//-----------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
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
    private GameObject dirtMoundPrefab;

    private bool PrefabsLoaded = false;

    private GameObject previousSpawnedPrefab;
    public string previousTrackedImage;
    private GameObject spawnedPrefab;
    public Dictionary<string, List<GameObject>> spawnedPrefabs = new Dictionary<string, List<GameObject>>();

    public Dictionary<GameObject, GameObject> spawnedObjects = new Dictionary<GameObject, GameObject>();

    private void Start()
    {
        if (trackedImageManager != null)
        {
            trackedImageManager.trackablesChanged.AddListener(OnImageChanged);
        }
    }

    public void SetupPrefab(List<Artifact> artifacts, string trackedImageName, bool finalPrefabList = false)
    {
        List<GameObject> prefabs = new List<GameObject>();
        Debug.Log("Setting up Prefabs");
        try{
            foreach(Artifact artifact in artifacts) 
            {
                GameObject prefab = artifact.artifactPrefab;
                Debug.Log("Found a prefab");

                GameObject newPrefab = Instantiate(prefab);
                newPrefab.name = prefab.name;
                newPrefab.SetActive(false);
                prefabs.Add(newPrefab);
                spawnedObjects.Add(newPrefab, prefab);
                ArtifactInfo newArtifactInfo = newPrefab.AddComponent<ArtifactInfo>();
                newArtifactInfo.artifact = artifact.artifact;
                newArtifactInfo.trackedImageName = trackedImageName;

                GameObject newDirtMoundPrefab = Instantiate(dirtMoundPrefab); // Add in dirt mound
                newArtifactInfo.dirtMound = newDirtMoundPrefab;
                newDirtMoundPrefab.name = "Dirt Mound";
                newDirtMoundPrefab.transform.parent = newPrefab.transform;
                newDirtMoundPrefab.transform.position = new Vector3(0,0,0);
            }
        } 
        catch
        {
            Debug.Log("There is no artifacts to get or something went wrong");
        }
        spawnedPrefabs.Add(trackedImageName, prefabs);
        PrefabsLoaded = finalPrefabList;
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
                    spawnedPrefab.transform.SetParent(null);
                    spawnedPrefab.SetActive(false);
                } catch (MissingReferenceException)
                {
                    print("Object has been deleted");
                }
                SetUIActive(false);
                spawnedPrefab = null;
                trackedObject = null;
            }
            else if (trackedImage.trackingState == TrackingState.Tracking)
            {
                //Enable the associated content
                try {
                    if (trackedImage.referenceImage.name != previousTrackedImage || previousSpawnedPrefab == null)
                    {
                        spawnedPrefab = spawnedPrefabs[trackedImage.referenceImage.name][UnityEngine.Random.Range(0, spawnedPrefabs[trackedImage.referenceImage.name].Count)];
                        previousSpawnedPrefab = spawnedPrefab;
                        previousTrackedImage = trackedImage.referenceImage.name;
                    }
                    else
                    {
                        spawnedPrefab = previousSpawnedPrefab;
                    }
                    
                    if(spawnedPrefab.transform.parent != trackedImage.transform)
                    {
                        Debug.Log("Enabling associated content: " + spawnedPrefab.name);
                        spawnedPrefab.transform.SetParent(trackedImage.transform);
                        spawnedPrefab.transform.localPosition = spawnedObjects[spawnedPrefab].transform.localPosition;
                        spawnedPrefab.transform.localRotation = spawnedObjects[spawnedPrefab].transform.localRotation;

                        spawnedPrefab.SetActive(true);
                        trackedObject = spawnedPrefab;
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

    public void SetUIActive(bool value) // Enable the dig button and progress bar
    {
        DigButton.SetActive(value);
        ProgressBar.SetActive(value);
    }
}
