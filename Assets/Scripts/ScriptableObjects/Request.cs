using System;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "New Request", menuName = "Requests/New Request")]
public class Request : ScriptableObject
{
    public string[] RequestDialog;
    public GameItem[] RequestItem_Earn;
    public GameItem[] RequestItem_Give;
    public bool[] EarnItemWhenApproved;
    public bool[] GiveItemWhenApproved;
    public int[] EarnItemQuantity;
    public int[] GiveItemQuantity;
    public bool[] EarnItemNoMatterWhat;
    public RequestType RequestType;
    public RequestStats RequestStat;
}

[Serializable]
public struct RequestStats
{
    public Stats[] Stat;
}
[Serializable]
public struct Stats
{
    public int Palace;
    public int Nation;
    public int Personal;
    public int Fear;
}


public enum RequestType
{
    Help,
    Item,
    Money,
    Soldier,
}
