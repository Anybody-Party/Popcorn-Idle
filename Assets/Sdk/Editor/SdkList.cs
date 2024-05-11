using System.Collections.Generic;
using _Project.Scripts.Analytics.Editor;
using UnityEngine;

[CreateAssetMenu(menuName = "Sdk List")]
public class SdkList : ScriptableObject
{
    [SerializeField] private List<SdkConfiguration> _configurations;
    public IEnumerable<SdkConfiguration> Configurations => _configurations;
}