//-----------------------------------------
// Made by: John (1 Dec 2025)
// Description: This is mainly used to simplify the UI linking
//-----------------------------------------
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UIFunctions : MonoBehaviour
{
    private Button button;
    public GameObject Page;

    void Start()
    {
        button = GetComponent<Button>();
    }
    public void ChangePage(GameObject newPage)
    {
        Page.SetActive(false);
        newPage.SetActive(true);
    }
}
