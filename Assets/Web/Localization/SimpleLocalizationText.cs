using TMPro;
using UnityEngine;
using UnityEngine.Localization.Components;

[RequireComponent(typeof(LocalizeStringEvent))]
public class SimpleLocalizationText : MonoBehaviour
{
    private TextMeshProUGUI _text;
    private LocalizeStringEvent _localizeStringEvent;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
        _localizeStringEvent = GetComponent<LocalizeStringEvent>();
        _localizeStringEvent.OnUpdateString.AddListener(UpdateString);
        _localizeStringEvent.RefreshString();
    }

    private void UpdateString(string text)
    {
        _text.text = text;
    }
}