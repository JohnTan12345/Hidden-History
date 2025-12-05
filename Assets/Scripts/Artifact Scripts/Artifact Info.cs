using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class ArtifactInfo : MonoBehaviour
{
    public int digProgess = 0;


    private CancellationTokenSource timerStop;

    void OnEnable()
    {
        timerStop?.Cancel();

    }
    void OnDisable()
    {
        timerStop = new CancellationTokenSource();
        ResetCooldown(timerStop.Token);
    }
    private async void ResetCooldown(CancellationToken cancellationToken)
    {
        try {
            await Task.Delay(3000, cancellationToken);
            digProgess = 0;
        } catch (TaskCanceledException)
        {
            Debug.Log("task was cancelled");
        }
    }
}
