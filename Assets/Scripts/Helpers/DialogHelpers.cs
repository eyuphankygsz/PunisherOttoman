using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogHelpers : MonoBehaviour
{

    public static DialogHelpers Instance { get; private set; }

    public bool ConversationStarted { get; private set; }
    public bool ConversationEnded
    {
        get { return _convEnd; }
        private set
        {
            _convEnd = value;
            if (_convEnd)
                OnConversationEnd();
        }
    }
    public bool IsDialogueOver
    {
        get { return _isDialogOver; }
        private set
        {
            _isDialogOver = value;
            DialogManager.Instance.SkipDialogSetActive((!_isDialogOver && !_convEnd) || (_isDialogOver && !_convEnd));
        }
    }

    private bool _convEnd;
    private bool _isDialogOver = true;

    public Human CurrentHuman { get; private set; }
    int _requestLine;
    int _requestItemCountEarn, _requestItemCountGive;

    Request _request;

    [SerializeField] TextMeshProUGUI _displayTmp;
    string _displayText;

    const float _spellerDefaultTime = 0.05f;
    float _spellerTimer = _spellerDefaultTime;
    int _spelledWord = 0;


    bool _cantSelected;
    Request _requiredRequest;
    int _requiredID;

    [SerializeField] Animator _guardian;

    private void Awake()
    {
        if (GameObject.FindGameObjectsWithTag("DialogHelper").Length > 1)
            Destroy(gameObject);
        else
            Instance = this;

    }
    public void NewRequest(Human human)
    {
        _requestLine = 0;
        _requestItemCountEarn = 0;
        _requestItemCountGive = 0;

        CurrentHuman = human;
        _request = CurrentHuman.H_Request;

        _spellerTimer = _spellerDefaultTime;
        ConversationStarted = true;
        ConversationEnded = false;
    }
    public string GetNextDialog()
    {
        _spelledWord = 0;
        if (_request == null)
            return null;

        if (_requestLine == _request.RequestDialog.Length)
            return null;

        string request = "";
        string splittedRequest = _request.RequestDialog[_requestLine++];
        int oldChar = 0;
        for (int i = 0; i < splittedRequest.Length; i++)
        {
            if (splittedRequest[i] == '$')
            {
                request += splittedRequest[oldChar..i] + string.Format("<b>{0}</b>", _request.RequestItem_Give[_requestItemCountGive++].ItemName);
                oldChar = i + 1;
                continue;
            }
            if (splittedRequest[i] == '#')
            {
                request += splittedRequest[oldChar..i] + string.Format("<b>{0}</b>", _request.RequestItem_Earn[_requestItemCountEarn++].ItemName);
                oldChar = i + 1;
                continue;
            }
        }

        if (string.IsNullOrEmpty(request))
            request = splittedRequest;
        else
            request += splittedRequest[oldChar..splittedRequest.Length];

        _displayText = request;
        IsDialogueOver = false;
        return request;
    }

    public void Display()
    {
        if (_spelledWord != _displayText.Length)
        {
            _spellerTimer -= Time.deltaTime;
            if (_spellerTimer <= 0)
            {
                _spellerTimer = _spellerDefaultTime;
                _spelledWord++;
                _displayTmp.text = _displayText.Substring(0, _spelledWord);
            }
        }
        else
        {
            IsDialogueOver = true;

            if (_requestLine >= _request.RequestDialog.Length)
            {
                ConversationEnded = true;
            }
        }

    }
    public void DisplayWhole()
    {
        _spellerTimer = _spellerDefaultTime;
        _displayTmp.text = _displayText;
        IsDialogueOver = true;

        if (_requestLine >= _request.RequestDialog.Length)
        {
            ConversationEnded = true;
        }
    }
    public void DecisionMade(int id)
    {
        ApplyItemExchange(id);
        if (_cantSelected)
        {
            GiveError();
            return;
        }
        DialogManager.Instance.UpdateStats(id);

        if (id == 2)
            _guardian.SetTrigger("Took");
        CurrentHuman.ExitRoom();
    }
    void GiveError()
    {
        GameManager.Instance.NotEnough(_requiredRequest, _requiredID);
        DialogManager.Instance.SetButtonActive(true);

    }
    void ApplyItemExchange(int id)
    {
        _cantSelected = false;
        bool? whenApproved = id == 0 ? true : id == 1 ? false : null;

        for (int i = 0; i < _request.RequestItem_Give.Length; i++)
        {
            if (_request.GiveItemWhenApproved[i] == whenApproved)
            {
                // Debug.Log(whenApproved);
                // BackpackManager.Instance.AddItemToList(_request.RequestItem_Earn[i], _request.EarnItemQuantity[i]);

                bool matched = false;
                if (_request.RequestItem_Give[i].MultipleQuantity)
                {
                    if (BackpackManager.Instance.IsThisOnInventory(_request.RequestItem_Give[i], _request.GiveItemQuantity[i]))
                    {
                        BackpackManager.Instance.RemoveItemFromInventory(_request.RequestItem_Give[i], _request.GiveItemQuantity[i]);
                        matched = true;
                    }
                }
                else if (BackpackManager.Instance.IsThisOnInventory(_request.RequestItem_Give[i]))
                {
                    BackpackManager.Instance.RemoveItemFromInventory(_request.RequestItem_Give[i]);
                    matched = true;
                }

                if (!matched)
                {
                    _requiredRequest = _request;
                    _requiredID = i;
                    _cantSelected = true;
                    return;
                }
            }
        }
        for (int i = 0; i < _request.RequestItem_Earn.Length; i++)
        {
            if (_request.EarnItemWhenApproved[i] == whenApproved || (_request.EarnItemNoMatterWhat.Length > 0 && _request.EarnItemNoMatterWhat[i] == true))
            {
                BackpackManager.Instance.AddItemToList(_request.RequestItem_Earn[i], _request.EarnItemQuantity[i]);
            }
        }

    }
    void OnConversationEnd()
    {
        DialogManager.Instance.SkipDialogSetActive(false);
        DialogManager.Instance.SetButtonActive(true);
    }
}
