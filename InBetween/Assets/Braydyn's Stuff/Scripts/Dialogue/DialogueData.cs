using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewDialogue", menuName = "Dialogue/Advanced Dialogue")]
public class DialogueData : ScriptableObject
{
    public List<DialogueLine> lines = new List<DialogueLine>();
}

[System.Serializable]
public class DialogueLine
{
    [TextArea(2, 6)]
    public string sentence;

    public string speakerName;
    public Sprite portrait;

    [Tooltip("Optional: index of next line to jump to. Leave -1 to continue normally.")]
    public int nextLineIndex = -1;

    [Header("Voice Pitch Range")]
    [Range(0.01f, 2.0f)] public float minPitch = 0.95f;
    [Range(0.01f, 2.0f)] public float maxPitch = 1.05f;

    [Header("Typing Speed")]
    [Range(0.005f, 0.1f)] public float typingSpeed = 0.015f;
}

