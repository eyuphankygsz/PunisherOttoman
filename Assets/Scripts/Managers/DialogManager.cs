using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public static DialogManager Instance { get; private set; }

    [SerializeField] private Human[] _humanArray;
    private int _humanLine = 0;

    [SerializeField] private Image _palaceBar, _nationBar, _personalBar, _fearBar;
    [SerializeField] private Button[] _manageButtons;

    [SerializeField] private GameObject _spaceIcon;


    public Request[] _endRequests;
    private void Awake()
    {
        if (GameObject.FindGameObjectsWithTag("DialogManager").Length > 1)
            Destroy(gameObject);
        else
        {
            Instance = this;
            SetButtonActive(false);
        }
    }
    private void Start()
    {
        SetStats();
    }
    private void Update()
    {
        if (DialogHelpers.Instance.ConversationStarted && !DialogHelpers.Instance.IsDialogueOver)
            DialogHelpers.Instance.Display();

        if (Input.GetKeyDown(KeyCode.Space) && !DialogHelpers.Instance.IsDialogueOver)
            DialogHelpers.Instance.DisplayWhole();
        else if (Input.GetKeyDown(KeyCode.Space) && DialogHelpers.Instance.IsDialogueOver)
            DialogHelpers.Instance.GetNextDialog();

    }


    public void NextHuman()
    {
        if (_humanLine < _humanArray.Length)
            _humanArray[_humanLine].EnterRoom();
        else
            GameManager.Instance.EndDay();
    }
    public void SetButtonActive(bool active)
    {
        for (int i = 0; i < _manageButtons.Length; i++)
            _manageButtons[i].interactable = active;
    }
    public void UpdateStats(int statCount)
    {
        float palaceAmount =

        _palaceBar.fillAmount += (float)_humanArray[_humanLine].H_Request.RequestStat.Stat[statCount].Palace / 100;
        _nationBar.fillAmount += (float)_humanArray[_humanLine].H_Request.RequestStat.Stat[statCount].Nation / 100;
        _personalBar.fillAmount += (float)_humanArray[_humanLine].H_Request.RequestStat.Stat[statCount].Personal / 100;
        _fearBar.fillAmount += (float)_humanArray[_humanLine].H_Request.RequestStat.Stat[statCount].Fear / 100;

        _palaceBar.fillAmount = Mathf.Clamp(_palaceBar.fillAmount, 0f, 1f);
        _nationBar.fillAmount = Mathf.Clamp(_nationBar.fillAmount, 0f, 1f);
        _personalBar.fillAmount = Mathf.Clamp(_personalBar.fillAmount, 0f, 1f);
        _fearBar.fillAmount = Mathf.Clamp(_fearBar.fillAmount, 0f, 1f);


        _humanLine++;
    }

    public void SkipDialogSetActive(bool skip)
    {
        _spaceIcon.SetActive(skip);
    }

    public void SaveStats()
    {
        PlayerPrefs.SetFloat("Palace", _palaceBar.fillAmount);
        PlayerPrefs.SetFloat("Nation", _nationBar.fillAmount);
        PlayerPrefs.SetFloat("Personal", _personalBar.fillAmount);
        PlayerPrefs.SetFloat("Fear", _fearBar.fillAmount);
        PlayerPrefs.Save();
    }
    void SetStats()
    {
        _palaceBar.fillAmount = PlayerPrefs.GetFloat("Palace");
        _nationBar.fillAmount = PlayerPrefs.GetFloat("Nation");
        _personalBar.fillAmount = PlayerPrefs.GetFloat("Personal");
        _fearBar.fillAmount = PlayerPrefs.GetFloat("Fear");
    }

    public Request EndRequest()
    {
        Image lowestSlider = _palaceBar;
        Request selected = _endRequests[0];

        if (lowestSlider.fillAmount > _nationBar.fillAmount)
        {
            lowestSlider = _nationBar;
            selected = _endRequests[1];
        }

        if (lowestSlider.fillAmount > _personalBar.fillAmount)
        {
            lowestSlider = _personalBar;
            selected = _endRequests[2];
        }
        if (lowestSlider.fillAmount > _fearBar.fillAmount)
        {
            lowestSlider = _fearBar;
            selected = _endRequests[3];
        }

        return selected;
    }
}
