using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NameTagHandler : MonoBehaviour
{
    Animator _animator;
    TextMeshProUGUI _nameTmp;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _nameTmp = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }
    void ChangeName()
    {
       _nameTmp.text = DialogHelpers.Instance.CurrentHuman.H_Name + " " + DialogHelpers.Instance.CurrentHuman.H_Surname;
    }

    public void NameTagAnimation(bool play)
    {
        _animator.SetBool("OpenTag", play);
        ChangeName();
    }
}
