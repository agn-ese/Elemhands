using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecordManager : MonoBehaviour
{
    public String state = "not recording";
    public float waitTime = 3f;

    public float recordTime = 5f;
    public GameObject rightHand;
    public GameObject leftHand;

    [SerializeField] private TextMeshProUGUI timerText; // Aggiungi una variabile per il testo UI

    // Start is called before the first frame update
    void Start()
    {
        rightHand = GameObject.Find("RightHand");
        leftHand = GameObject.Find("LeftHand");
    }

    // Update is called once per frame
    void Update()
    {
        // se premo il tasto F aspetto waittime e poi abilito il componente record nelle due mani, smetto di registrare dopo recordTime secondi
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (state == "not recording")
            {
                state = "recording";
                StartCoroutine(StartRecording());
                StartCoroutine(UpdateTimer()); // Avvia la coroutine per aggiornare il timer
            }
            else
            {
                state = "not recording";
                rightHand.GetComponent<Record>().enabled = false;
                leftHand.GetComponent<Record>().enabled = false;
            }
        }

    }

    private IEnumerator StartRecording()
    {
        // aspetto waittime e poi abilito il componente record nelle due mani
        yield return new WaitForSeconds(waitTime);
        rightHand.GetComponent<Record>().enabled = true;
        leftHand.GetComponent<Record>().enabled = true;
        yield return new WaitForSeconds(recordTime);
        rightHand.GetComponent<Record>().enabled = false;
        leftHand.GetComponent<Record>().enabled = false;
    }

    private IEnumerator UpdateTimer()
    {
        float remainingTime = waitTime;
        while (remainingTime > 0)
        {
            timerText.text = "Tempo rimanente: " + remainingTime.ToString("F1") + "s";
            yield return new WaitForSeconds(0.1f);
            remainingTime -= 0.1f;
        }
        timerText.text = "Inizio registrazione!";

        remainingTime = recordTime;
        while (remainingTime > 0)
        {
            timerText.text = "Tempo rimanente: " + remainingTime.ToString("F1") + "s";
            yield return new WaitForSeconds(0.1f);
            remainingTime -= 0.1f;
        }
        timerText.text = "Registrazione terminata!";
    }
}


