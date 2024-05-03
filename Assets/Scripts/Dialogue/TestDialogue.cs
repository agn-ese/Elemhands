using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDialogue : MonoBehaviour
{
    public DialogueManager dialogueManager;
    public Dialogue dialogue;

    public void testDialogue()
    {
        dialogueManager.StartDialogue(dialogue);
    }
}
