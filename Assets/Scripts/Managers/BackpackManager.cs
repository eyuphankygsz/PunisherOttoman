using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackpackManager : MonoBehaviour
{
    public static BackpackManager Instance { get; private set; }

    [SerializeField] private GameObject _itemHolder;

    public GameObject Container { get { return _container; } set { _container = value; } }
    private GameObject _container;


    private static List<InventoryItem> CurrentInventoryItems = new List<InventoryItem>();

    [HideInInspector] public List<ItemHolder> ItemHolderList = new List<ItemHolder>();


    private void Awake()
    {
        if (GameObject.FindGameObjectsWithTag("BackpackManager").Length > 1)
            Destroy(gameObject);
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    public void ResetInventory()
    {
        ItemHolderList.Clear();
    }
    public void AddItemToList(GameItem item, int quantity)
    {
        //stacklenebilir
        if (item.MultipleQuantity)
        {
            bool matched = false;
            for (int i = 0; i < CurrentInventoryItems.Count; i++)
            {
                if (item.ID == CurrentInventoryItems[i].Item.ID)
                {
                    matched = true;
                    CurrentInventoryItems[i].Quantity += quantity;
                    ReSetupItems();
                }
            }
            if (!matched)
            {
                AddTolist(item, quantity);
            }
        }
        else
        {
            AddTolist(item, quantity);
        }
    }
    public void AddTolist(GameItem item, int quantity)
    {
        InventoryItem invItem = new InventoryItem();
        invItem.Setup(item, quantity);
        CurrentInventoryItems.Add(invItem);
        AddOneItemToInventory(invItem);
    }
    public void AddItemToInventory()
    {
        for (int i = 0; i < CurrentInventoryItems.Count; i++)
        {
            GameObject newItem = Instantiate(_itemHolder, _container.transform);
            newItem.AddComponent<ItemHolder>();
            newItem.GetComponent<ItemHolder>().Setup(CurrentInventoryItems[i].Item, CurrentInventoryItems[i].Quantity);
            ItemHolderList.Add(newItem.GetComponent<ItemHolder>());
        }
    }

    public void AddOneItemToInventory(InventoryItem item)
    {
        GameObject newItem = Instantiate(_itemHolder, _container.transform);
        newItem.AddComponent<ItemHolder>();
        newItem.GetComponent<ItemHolder>().Setup(item.Item, item.Quantity);
        ItemHolderList.Add(newItem.GetComponent<ItemHolder>());
    }
    public void RemoveItemFromInventory(GameItem item, int quantity)
    {
        for (int i = 0; i < CurrentInventoryItems.Count; i++)
            if (CurrentInventoryItems[i].Item.ID == item.ID)
            {
                CurrentInventoryItems[i].Quantity -= quantity;
                if (CurrentInventoryItems[i].Quantity == 0)
                    RemoveWholeItem(item);

                ReSetupItems();
            }
    }
    public void RemoveItemFromInventory(GameItem item)
    {
        for (int i = 0; i < CurrentInventoryItems.Count; i++)
            if (CurrentInventoryItems[i].Item.ID == item.ID)
            {
                RemoveWholeItem(item);
                ReSetupItems();
            }
    }
    void RemoveWholeItem(GameItem item)
    {
        for (int i = 0; i < ItemHolderList.Count; i++)
        {
            if (ItemHolderList[i].Item == item)
            {
                ItemHolder holder = ItemHolderList[i];
                ItemHolderList.RemoveAt(i);
                CurrentInventoryItems.RemoveAt(i);

                Destroy(holder.gameObject);
                break;
            }
        }

        for (int i = 0; i < ItemHolderList.Count; i++)
            Debug.Log(ItemHolderList[i].Item.ItemName);

        for (int i = 0; i < CurrentInventoryItems.Count; i++)
            Debug.Log(CurrentInventoryItems[i].Item.ItemName);

        for (int i = 0; i < CurrentInventoryItems.Count; i++)
        {
            Debug.Log(CurrentInventoryItems[i]);
            Debug.Log(ItemHolderList[i]);
        }
    }
    public void ReSetupItems()
    {
        for (int i = 0; i < ItemHolderList.Count; i++)
        {
            ItemHolderList[i].Setup(CurrentInventoryItems[i].Item, CurrentInventoryItems[i].Quantity);
        }
    }
    public bool IsThisOnInventory(GameItem item, int quantity)
    {
        for (int i = 0; i < CurrentInventoryItems.Count; i++)
            if (item.ID == CurrentInventoryItems[i].Item.ID)
                if (quantity <= CurrentInventoryItems[i].Quantity)
                    return true;
        return false;
    }
    public bool IsThisOnInventory(GameItem item)
    {
        for (int i = 0; i < CurrentInventoryItems.Count; i++)
            if (item.ID == CurrentInventoryItems[i].Item.ID)
                return true;
        return false;
    }

    public void ResetItemHolders()
    {
        ItemHolderList.Clear();
    }

}
