using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PaintingGalleryManager : MonoBehaviour
{
    public PaintingData[] paintings;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI infoText;
    
    public Image[] elementSlots;

    private int currentIndex = 0;
    private bool[] collected = new bool[4];

    public void ShowPainting(int index)
    {
        currentIndex = index;
        PaintingData data = paintings[index];

        titleText.text = data.title;
        infoText.text = data.info;

        collected = new bool[4];
        for (int i = 0; i < elementSlots.Length; i++)
        {
            elementSlots[i].sprite = data.elementSilhouettes[i];
        }
    }

    public void NextPainting()
    {
        currentIndex = (currentIndex + 1) % paintings.Length;
        ShowPainting(currentIndex);
    }

    public void PreviousPainting()
    {
        currentIndex = (currentIndex - 1 + paintings.Length) % paintings.Length;
        ShowPainting(currentIndex);
    }

    public void CollectElement(int elementIndex)
    {
        collected[elementIndex] = true;
        elementSlots[elementIndex].sprite = paintings[currentIndex].elementSprites[elementIndex];

        if (AllCollected())
        {
            RevealPainting();
        }
    }

    private bool AllCollected()
    {
        foreach (bool c in collected) if (!c) return false;
        return true;
    }

    private void RevealPainting()
    {
        for (int i = 0; i < elementSlots.Length; i++)
        {
            elementSlots[i].sprite = paintings[currentIndex].fullPainting;
        }
    }
}
