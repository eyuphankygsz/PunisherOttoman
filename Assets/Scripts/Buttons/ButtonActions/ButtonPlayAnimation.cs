using System;
using UnityEngine;
using UnityEngine.UI;

public class ButtonAnimationBool : MonoBehaviour, IButtonSet
{

    [SerializeField] private Animator _animator;
    [SerializeField] private AnimationArray _animationArray;
    [SerializeField] private bool _canReverse;
    private bool _turn = false;

    public void OnActivate()
    {
        for (int i = 0; i < _animationArray.AnimationName.Length; i++)
            PlayAnimation(i);

        _turn = _canReverse ? !_turn : _turn;
    }

    void PlayAnimation(int i)
    {
        switch (_animationArray.AnimationType[i])
        {
            case AnimationType.Bool:
                if (Convert.ToBoolean(_animationArray.WhenToPlay[i]) == _turn)
                    _animator.SetBool(_animationArray.AnimationName[i], Convert.ToBoolean(_animationArray.AnimationParameter[i]));
                break;
            case AnimationType.Float:
                if (Convert.ToBoolean(_animationArray.WhenToPlay[i]) == _turn)
                    _animator.SetFloat(_animationArray.AnimationName[i], Convert.ToSingle(_animationArray.AnimationParameter[i]));
                break;
            case AnimationType.Int:
                if (Convert.ToBoolean(_animationArray.WhenToPlay[i]) == _turn)
                    _animator.SetInteger(_animationArray.AnimationName[i], Convert.ToInt32(_animationArray.AnimationParameter[i]));
                break;
            case AnimationType.Trigger:
                if (Convert.ToBoolean(_animationArray.WhenToPlay[i]) == _turn)
                    _animator.SetTrigger(_animationArray.AnimationName[i]);
                break;
        }
    }
    void Awake()
    {
        GetComponent<Button>().onClick.AddListener(OnActivate);
    }

}
