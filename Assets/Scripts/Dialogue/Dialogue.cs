using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Dialogue
{
    public string name;

    [TextArea(3, 10)]
    public string sentence;

    public Sprite image;

    public AudioClip audio;

    public UnityEvent onDialogueStart;

}
