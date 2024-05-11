using System;
using UnityEngine;

namespace _Project.Scripts.Analytics.Editor
{
    [Serializable]
    public class SdkConfiguration
    {
        public SdkConfiguration(string define, string name)
        {
            Define = define;
            Name = name;
        }

        [field: SerializeField] public string Define { get; private set; }
        [field: SerializeField] public string Name { get; private set; }
    }
}