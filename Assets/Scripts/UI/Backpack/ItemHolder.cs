using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemHolder : MonoBehaviour
{
    public GameItem Item { get; private set; }
    public int Quantity { get; private set; }

    public string GetName
    {
        get
        {
            return Item.ItemName;
        }
    }
    public int GetID
    {
        get
        {
            return Item.ID;
        }
    }
    public bool IsMultipleQuantity
    {
        get
        {
            return Item.MultipleQuantity;
        }
    }

    public void Setup(GameItem item, int quantity)
    {
        Item = item;
        Quantity = quantity;
        transform.GetChild(0).GetComponent<Image>().sprite = item.ItemSprite;
        transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = quantity.ToString();
    }
}
