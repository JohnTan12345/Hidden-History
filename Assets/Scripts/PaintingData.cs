using UnityEngine;

[CreateAssetMenu(fileName = "PaintingData", menuName = "TreasureHunt/PaintingData")]
public class PaintingData : ScriptableObject
{
    public string title;
    public string info;
    public Sprite[] elementSprites;
    public Sprite[] elementSilhouettes;
    public Sprite fullPainting;
    
}
