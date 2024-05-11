using UnityEngine;
using UnityEngine.UI;

public class Music : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private Image _image;
    [SerializeField] private Sprite _iconEnabled;
    [SerializeField] private Sprite _iconDisabled;
    [SerializeField] private Button _button;

    public bool Enabled { get; private set; }

    private void Awake()
    {
        _button.onClick.AddListener(OnButtonClick);
#if GAME_DISTRIBUTION
        GameDistribution.OnPauseGame += TurnOff;
#elif GAME_MONETIZE
        GameMonetize.OnPauseGame += TurnOff;
#endif
        Enabled = PlayerPrefs.GetInt(nameof(Music), 1) == 1;
        UpdateStatus();
    }

    public void TurnOff()
    {
        if (!Enabled) return;

        Enabled = false;
        UpdateStatus();
#if GAME_DISTRIBUTION
        GameDistribution.OnResumeGame += TurnOnAudio;
#elif GAME_MONETIZE
        GameMonetize.OnResumeGame += TurnOnAudio;
#endif
    }

    public void TurnOn()
    {
        Enabled = true;
        UpdateStatus();
#if GAME_DISTRIBUTION
        GameDistribution.OnResumeGame -= TurnOn;
#elif GAME_MONETIZE
        GameMonetize.OnResumeGame -= TurnOn;
#endif
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(OnButtonClick);
#if GAME_DISTRIBUTION
        GameDistribution.OnPauseGame -= TurnOff;
#elif GAME_MONETIZE
        GameMonetize.OnPauseGame -= TurnOff;
#endif
    }

    private void UpdateStatus()
    {
        if (Enabled)
            _audioSource.Play();
        else
            _audioSource.Pause();

        _image.sprite = Enabled ? _iconEnabled : _iconDisabled;
    }

    private void OnButtonClick()
    {
        Enabled = !Enabled;
        PlayerPrefs.SetInt(nameof(Music), Enabled ? 1 : 0);
        UpdateStatus();
    }
}