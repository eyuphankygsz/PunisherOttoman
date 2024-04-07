using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item/New Item")]
public class GameItem : ScriptableObject
{
    public int ID;
    public Sprite ItemSprite;
    public string ItemName;
    public bool MultipleQuantity;
}
