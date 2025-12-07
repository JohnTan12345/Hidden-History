using UnityEngine;

public class MainEvent : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        new User().CreateNewUserAsync("u4trgfd");
    }
}
