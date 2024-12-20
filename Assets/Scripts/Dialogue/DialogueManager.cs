using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    //public InputActionProperty triggerNext;
    public GameObject dialogueCanvas;

    public AudioSource audioSource;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;

    private Queue<Dialogue> dialogues;

    private string stato = "stop";

    public UnityEvent onFineDialogo;

    private Image image;

    void Start()
    {
        dialogues = new Queue<Dialogue>();
        // prendo dal figlio Image il componente Image
        image = dialogueCanvas.transform.Find("Image").GetComponent<Image>();
    }


    public void StartDialogue(Dialogue[] dialoguesInput)
    {
        //Debug.Log("inizio dialogo con" + dialogue.name);

        stato = "dialogo";

        dialogueCanvas.SetActive(true);

        dialogues.Clear();

        foreach (Dialogue dial in dialoguesInput)
        {

            dialogues.Enqueue(dial);

        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (stato == "dialogo")
        {
            if (dialogues.Count == 0)
            {
                EndDialogue();
                return;
            }

            Dialogue dialogue = dialogues.Dequeue();

            StopAllCoroutines();
            StartCoroutine(TypeSentence(dialogue.sentence, dialogue.name));

            if (dialogue.image != null)
            {
                image.enabled = true;
                image.sprite = dialogue.image;
            }
            else
            {
                image.enabled = false;
                image.sprite = null;
            }

            if (dialogue.audio != null)
            {
                audioSource.clip = dialogue.audio;
                audioSource.Play();
            }

            if (dialogue.onDialogueStart != null)
            {
                dialogue.onDialogueStart.Invoke();
            }
        }



    }

    public void SoundGestureNextSentence(AudioSource source)
    {
        if (stato == "dialogo")
        {
            if (source != null)
            {
                source.Play();
            }
        }
    }

    IEnumerator TypeSentence(string sentence, string name)
    {
        nameText.text = name;
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            dialogueText.text = dialogueText.text.Replace("\r", " ").Replace("\n", " ");
            yield return null;
        }
    }

    public void EndDialogue()
    {
        //Debug.Log("fine dialogo");
        stato = "stop";
        audioSource.Stop();
        dialogueCanvas.SetActive(false);
        onFineDialogo.Invoke();

    }


}
