using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;

public class DataManager : MonoBehaviourSingleton<DataManager>
{
    [SerializeField] private MainData mainData;
    [SerializeField] private BalanceData balanceData;
    [SerializeField] private SettingsData settingsData;

    public MainData MainData { get => mainData; set => mainData = value; }
    public BalanceData BalanceData { get => balanceData; set => balanceData = value; }
    public SettingsData SettingsData { get => settingsData; set => settingsData = value; }

    private void Awake()
    {
        Debug.Log(Utility.GetDataPath());
        LoadData();
    }

    private void SaveData()
    {
        MainData.SaveData();
        SettingsData.SaveData();
    }

    private void LoadData()
    {
        MainData.LoadData();
        SettingsData.LoadData();
    }

    [NaughtyAttributes.Button]
    public void ResetData()
    {
        MainData.ResetData();
    }

#if UNITY_EDITOR
    [ExecuteInEditMode]
    [MenuItem("Tools/DeleteAllGameData")]
    public static void DeleteAllGameData()
    {
        if (Directory.Exists(Utility.GetDataPath()))
            Directory.Delete(Utility.GetDataPath(), true);
    }
#endif

    private void OnApplicationQuit()
    {
        SaveData();
#if UNITY_EDITOR
        //DeleteAllGameData(); // TODO: REMOVE
#endif
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
            SaveData();
    }
}

