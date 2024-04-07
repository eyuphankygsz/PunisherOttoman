using UnityEngine;

public class InventoryItem
{
    public GameItem Item { get; private set; }
    public int Quantity { get { return _quantity; } set { _quantity = value;} }
    private int _quantity;

    public void Setup(GameItem item, int quantity)
    {
        Item = item;
        Quantity = quantity;
    }
}
