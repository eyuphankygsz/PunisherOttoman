using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }
    [SerializeField] private AudioMixer _audioMixer;

    private Slider _music, _audio;

    private List<AudioSource> _audioSources = new List<AudioSource>();
    private int _audioLine = 0;
    void Awake()
    {
        if (GameObject.FindGameObjectsWithTag("SoundManager").Length > 1)
            Destroy(gameObject);
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        CreateSources();
    }
    public void Setup(Slider music, Slider audio)
    {
        if (!PlayerPrefs.HasKey("Music"))
        {
            PlayerPrefs.SetFloat("Music", 0.5f);
            PlayerPrefs.SetFloat("Audio", 1);
        }

        _music = music;
        _music.onValueChanged.AddListener(delegate { ChangeMusic(); });
        _audio = audio;
        _audio.onValueChanged.AddListener(delegate { ChangeAudio(); });

        _music.value = PlayerPrefs.GetFloat("Music");
        _audio.value = PlayerPrefs.GetFloat("Audio");
    }

    void ChangeMusic()
    {
        if (_music.value <= 0)
            _music.value = 0.0001f;

        _audioMixer.SetFloat("Music", Mathf.Log10(_music.value) * 20);
        PlayerPrefs.SetFloat("Music", _music.value);
    }
    void ChangeAudio()
    {
        if (_audio.value <= 0)
            _audio.value = 0.0001f;

        _audioMixer.SetFloat("Audio", Mathf.Log10(_audio.value) * 20);
        PlayerPrefs.SetFloat("Audio", _audio.value);
    }

    void CreateSources()
    {
        for (int i = 0; i < 6; i++)
        {
            _audioSources.Add(gameObject.AddComponent<AudioSource>());
            _audioSources[i].outputAudioMixerGroup = _audioMixer.FindMatchingGroups("Audio")[0];
        }
    }

    public void PlayAudio(AudioClip clip)
    {
        if (!_audioSources[_audioLine].isPlaying)
        {
            _audioSources[_audioLine].clip = clip;
            _audioSources[_audioLine].Play();
            _audioLine = (_audioLine + 1) % _audioSources.Count; 
        }
        else
        {
            PlayAudio(clip, _audioLine);
        }
    }
    public void PlayAudio(AudioClip clip, int attempt)
    {
        attempt++;
        if (attempt == _audioSources.Count)
            CreateNewAudio(clip);

        if (!_audioSources[attempt].isPlaying)
        {
            _audioSources[attempt].clip = clip;
            _audioSources[attempt].Play();
            _audioLine = (_audioLine + 1) % _audioSources.Count;
        }
        else
            PlayAudio(clip, attempt);
    }
    void CreateNewAudio(AudioClip clip)
    {
        AudioSource src = gameObject.AddComponent<AudioSource>();
        src.clip = clip;
        _audioSources.Add(src);
    }
}
