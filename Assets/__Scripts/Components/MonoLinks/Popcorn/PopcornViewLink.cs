using Leopotam.Ecs;
using System;
using UnityEngine;

[Serializable]
public struct PopcornViewLink
{
    [Header("Bodies")]
    public GameObject RawBody;
    public GameObject DoneBody;
    public GameObject BaseBody;

    [Header("Emotions")]
    public GameObject SmileEmotion;
    public GameObject HappyEmotion;
    public GameObject ScaryEmotion;

    [Header("Additions")]
    public GameObject Chocolate;
    public GameObject Salt;
    public GameObject Caramel;
    public GameObject Wasabi;
}