using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class DialogueManager : MonoBehaviour
{
    //public InputActionProperty triggerNext;
    public GameObject dialogueCanvas;

    public AudioSource audioSource;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;

    private Queue<string> sentences;
    private Queue<AudioClip> audios;

    private string stato = "stop";

    public UnityEvent onFineDialogo;

    void Start()
    {
        sentences = new Queue<string>();
        audios = new Queue<AudioClip>();
    }

    void Update()
    {
        // va aggiustato qui
        /* if (stato == "dialogo" && triggerNext.action.triggered)
        {
            DisplayNextSentence();
        } */
    }

    public void StartDialogue(Dialogue dialogue)
    {
        Debug.Log("inizio dialogo con" + dialogue.name);

        stato = "dialogo";

        dialogueCanvas.SetActive(true);

        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);

        }

        if (dialogue.audios != null)
        {
            foreach (AudioClip audio in dialogue.audios)
            {
                audios.Enqueue(audio);
            }

        }



        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (stato == "dialogo")
        {
            if (sentences.Count == 0)
            {
                EndDialogue();
                return;
            }

            AudioClip audio = null;

            string sentence = sentences.Dequeue();

            if (audios != null && audios.Count != 0)
            {
                audio = audios.Dequeue();
            }
            else
            {
                audio = null;
            }


            //Debug.Log(sentence);
            StopAllCoroutines();
            StartCoroutine(TypeSentence(sentence));
            if (audio != null)
            {
                audioSource.clip = audio;
                audioSource.Play();
            }
        }



    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
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
