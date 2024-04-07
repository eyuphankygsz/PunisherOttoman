using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameEnder : MonoBehaviour
{
    [SerializeField]
    private Animator _blackness;

    public void EndGame()
    {
        _blackness.SetTrigger("EndGame");
    }
}
