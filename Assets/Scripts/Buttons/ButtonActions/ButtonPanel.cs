using UnityEngine;
using UnityEngine.UI;

public class ButtonPanelOpen : MonoBehaviour, IButtonSet
{
    [SerializeField] private GameObject[] _panelsToOpen;
    [SerializeField] private GameObject[] _panelsToClose;
    [SerializeField] private bool _canReverse;
    [SerializeField] private bool _stopTime;
    [SerializeField] private bool _stopWhenOpenPanel;

    private bool _turn;
    public void OnActivate()
    {
        for (int i = 0; i < _panelsToOpen.Length; i++)
            _panelsToOpen[i].SetActive(!_turn);
        for (int i = 0; i < _panelsToClose.Length; i++)
            _panelsToClose[i].SetActive(_turn);

        SetTimeScale();

        _turn = _canReverse ? !_turn : _turn;
    }

    void SetTimeScale()
    {
        if (_stopTime)
        {
            if (Time.timeScale == 1f && !_turn && _stopWhenOpenPanel)
                Time.timeScale = 0;
            else if (Time.timeScale == 1f && _turn && !_stopWhenOpenPanel)
                Time.timeScale = 0;
            else
                Time.timeScale = 1;
        }

    }

    void Awake()
    {
        GetComponent<Button>().onClick.AddListener(OnActivate);
    }

}
