using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonStartDay : MonoBehaviour, IButtonSet
{
    public void OnActivate()
    {
        GameManager.Instance.StartDay();
        gameObject.SetActive(false);
    }

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(OnActivate);
    }
}
