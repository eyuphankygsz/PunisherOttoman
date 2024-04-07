using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Blackness : MonoBehaviour
{
    Animator _anim;
    [SerializeField]
    private string _level;
    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }
    
    public void Change()
    {
        _anim.SetTrigger("Change");
    }

    void ChangeScene()
    {
        SceneManager.LoadScene(_level);
    }
}
