using UnityEditor;
using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    private void Start()
    {
        IncreaseTargetFrameRate();
        CreateSystems();
    }

    private static void IncreaseTargetFrameRate() =>
        Application.targetFrameRate = 60;

    private static void CreateSystems()
    {
        //GameLogic
        new ExampleMoneySystem();
        new VibrationSystem();
    }
}
