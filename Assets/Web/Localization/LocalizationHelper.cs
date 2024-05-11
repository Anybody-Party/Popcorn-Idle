using System.Text.RegularExpressions;
using NaughtyAttributes;
using TMPro;
#if UNITY_EDITOR
using UnityEditor.Localization;
#endif
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.UI;

public class LocalizationHelper : MonoBehaviour
{
    private const string TableName = "Default";
    private Text _text;
    private TextMeshProUGUI _textMesh;
    private LocalizeStringEvent _localizeStringEvent;

    private void Awake()
    {
        _text = GetComponent<Text>();
        _textMesh = GetComponent<TextMeshProUGUI>();
        if (TryGetComponent(out _localizeStringEvent))
            _localizeStringEvent.OnUpdateString.AddListener(UpdateString);
    }

    private void UpdateString(string text)
    {
        if (_text != null)
            _text.text = text;
        if (_textMesh != null)
            _textMesh.text = text;
    }

#if UNITY_EDITOR
    [Button]
    [ContextMenu("Execute")]
    private void MapTableEntryForLocalization()
    {
        _text = GetComponent<Text>();
        _textMesh = GetComponent<TextMeshProUGUI>();
        string text = _text != null ? _text.text : _textMesh.text;
        string key = GenerateKeyFromText(text);
        StringTableCollection tableCollection = LocalizationEditorSettings.GetStringTableCollection(TableName);

        if (tableCollection.SharedData.GetEntry(key) == null)
        {
            tableCollection.SharedData.AddKey(key);
            tableCollection.StringTables[0].AddEntry(key, text);
        }

        _localizeStringEvent = TryGetComponent(out LocalizeStringEvent localizeStringEvent)
            ? localizeStringEvent
            : gameObject.AddComponent<LocalizeStringEvent>();
        _localizeStringEvent.StringReference.TableReference = tableCollection.TableCollectionNameReference;
        _localizeStringEvent.StringReference.TableEntryReference = key;

        //DestroyImmediate(this);
    }

    [Button]
    [ContextMenu("Remove LocalizeStringEvent component if has no reference")]
    private void RemoveLocalizeStringEventIfHasNoReference()
    {
        _text = GetComponent<Text>();
        _textMesh = GetComponent<TextMeshProUGUI>();
        string text = _text != null ? _text.text : _textMesh.text;
        string key = GenerateKeyFromText(text);
        StringTableCollection tableCollection = LocalizationEditorSettings.GetStringTableCollection(TableName);

        if (TryGetComponent(out SimpleLocalizationText simpleLocalizationText) && tableCollection.SharedData.GetEntry(key) == null)
            DestroyImmediate(simpleLocalizationText);
        if (TryGetComponent(out _localizeStringEvent) && tableCollection.SharedData.GetEntry(key) == null)
            DestroyImmediate(_localizeStringEvent);
    }

    private static string GenerateKeyFromText(string text)
    {
        text = text.Replace('\n', '_').Replace(' ', '_');
        return Regex.Replace(text, "_+", "_");
    }
#endif
}