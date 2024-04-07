using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonDecision : MonoBehaviour,IButtonSet
{
    [SerializeField] private int id;
    public void OnActivate()
    {
        DialogManager.Instance.SetButtonActive(false);
        DialogHelpers.Instance.DecisionMade(id);
    }
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(OnActivate);
    }
}
