using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public NameTagHandler TagHandler { get; private set; }

    [SerializeField] private Blackness _blackness;
    [SerializeField] private TextMeshProUGUI _dayTmp;

    [SerializeField] private GameObject _container;

    [SerializeField] private GameObject _requirePanel;
    private TextMeshProUGUI _requireTmp;

    [SerializeField] private Slider _music, _audio;

    [SerializeField] private AudioClip _humanEnter, _humanExit;

    [field: SerializeField] public bool EndGame { get; private set; }

    void Awake()
    {
        if (GameObject.FindGameObjectsWithTag("GameManager").Length > 1)
            Destroy(gameObject);
        else
        {
            Instance = this;
            TagHandler = GameObject.FindGameObjectWithTag("NameTag").GetComponent<NameTagHandler>();
        }

        _dayTmp.text = "Gün " + (SceneManager.GetActiveScene().buildIndex);
        _requireTmp = _requirePanel.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        GetStats();


    }

    private void Start()
    {
        SetSlider();
        BackpackManager.Instance.Container = _container;
        BackpackManager.Instance.ResetInventory();
        BackpackManager.Instance.AddItemToInventory();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
            EndDay();
    }
    public void StartDay()
    {
        DialogManager.Instance.NextHuman();

    }

    public void EndDay()
    {
        SaveStats();
        _blackness.Change();
    }

    void SaveStats()
    {
        DialogManager.Instance.SaveStats();
    }
    void GetStats()
    {
        if (PlayerPrefs.HasKey("Palace"))
            return;

        PlayerPrefs.SetFloat("Palace", 0.45f);
        PlayerPrefs.SetFloat("Nation", 0.3f);
        PlayerPrefs.SetFloat("Personal", 0.5f);
        PlayerPrefs.SetFloat("Fear", 0.05f);
        PlayerPrefs.Save();

    }
    void SetSlider()
    {
        SoundManager.Instance.Setup(_music, _audio);
    }
    public void NotEnough(Request requiredRequest, int requestID)
    {
        _requireTmp.text = requiredRequest.GiveItemQuantity[requestID] + " " + requiredRequest.RequestItem_Give[requestID].ItemName + " maalesef envanterinizde yok!";
        _requirePanel.SetActive(true);
    }
    public void EnterHumanSound()
    {
        SoundManager.Instance.PlayAudio(_humanEnter);
    }
    public void ExitHumanSound()
    {
        SoundManager.Instance.PlayAudio(_humanExit);
    }

}
