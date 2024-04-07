using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : MonoBehaviour
{
    [field: SerializeField] public string H_Name { get; private set; }
    [field: SerializeField] public string H_Surname { get; private set; }
    [field: SerializeField] public Request H_Request { get; private set; }
    
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    public void EnterRoom()
    {
        CheckEndGame();
        gameObject.SetActive(true);
    }
    public void ExitRoom()
    {
        _animator.SetTrigger("ExitRoom");
        GameManager.Instance.TagHandler.NameTagAnimation(false);
    }
    void NextPerson()
    {
        DialogManager.Instance.NextHuman();
        gameObject.SetActive(false);
    }
    //Run by _animator.SetTrigger("EnterRoom");
    void NewConversation()
    {
        DialogHelpers.Instance.NewRequest(this);
        DialogHelpers.Instance.GetNextDialog();
        GameManager.Instance.TagHandler.NameTagAnimation(true);
        GameManager.Instance.EnterHumanSound();
    }
    void ExitSound()
    {
        GameManager.Instance.ExitHumanSound();
    }

    void CheckEndGame()
    {
        if (GameManager.Instance.EndGame)
        {
            H_Request = DialogManager.Instance.EndRequest();
        }
    }
}
