using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour, IButtonSet
{
    [SerializeField] private AudioClip _clip;
    public void OnActivate()
    {
        SoundManager.Instance.PlayAudio(_clip);
    }

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(OnActivate);
    }
}
