using UnityEngine;

public class MainEvent : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Auth.CreateNewAccountAsync("wallahi@gmail.com", "2332");
    }
}
