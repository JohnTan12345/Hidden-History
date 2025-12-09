//-----------------------------------------------------------------------------------------------------------------
// Created By: John Tan
// Description: Artifact info
//-----------------------------------------------------------------------------------------------------------------

using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class ArtifactInfo : MonoBehaviour
{
    public int digProgess = 0;
    public GameObject dirtMound;

    private CancellationTokenSource resetTimerCancelSource;

    void OnEnable() // Stop timer if enabled again
    {
        resetTimerCancelSource?.Cancel();

    }
    void OnDisable() // Start timer if disabled
    {
        resetTimerCancelSource = new CancellationTokenSource();
        ResetCooldown(3, resetTimerCancelSource.Token);
    }
    private async void ResetCooldown(float timeInSeconds, CancellationToken cancellationToken) // Timer for 3 seconds
    {
        try {
            await Task.Delay((int)(timeInSeconds * 1000), cancellationToken);
            digProgess = 0;
        } catch (TaskCanceledException)
        {
            Debug.Log("Reset Cooldown has been reset");
        }
    }
}
