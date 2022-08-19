using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;

public class GameData : MonoBehaviourSingleton<GameData>
{
    public StaticData StaticData;
    public SceneData SceneData;
    public RuntimeData RuntimeData;
    public PlayerData PlayerData;

    private void Awake()
    {
        Debug.Log(Utility.GetDataPath());

        RuntimeData = new RuntimeData();
        PlayerData = new PlayerData();
        PlayerData.Init();

        LoadData();
    }

    private void SaveData()
    {
        PlayerData.SaveData();
    }

    private void LoadData()
    {
        PlayerData.LoadData();
    }

    [NaughtyAttributes.Button]
    [ExecuteInEditMode]
    public void ResetData()
    {
        PlayerData.ResetData();
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
        DeleteAllGameData(); // TODO: REMOVE
#endif
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
            SaveData();
    }
}

