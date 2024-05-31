using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TutorialManager : MonoBehaviour
{
    private DialogueManager dialogueManager;
    public Dialogue[] dialogoInizio;
    public Dialogue[] dialogoObbiettivoRaggiunto;
    public Dialogue[] dialogoCassa;
    public bool obiettivoRaggiunto;
    public bool cassaAlzata;

    private void Start()
    {
        dialogueManager = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
        StartCoroutine(LateStart(1 / 2));
    }

    IEnumerator LateStart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        dialogueManager.StartDialogue(dialogoInizio);
    }

    public void DialogoObiettivoRaggiunto()
    {
        if (obiettivoRaggiunto == false)
        {
            obiettivoRaggiunto = true;
            dialogueManager.StartDialogue(dialogoObbiettivoRaggiunto);
        }

    }

    public void DialogoCassaSollevata()
    {
        if (cassaAlzata == false)
        {
            cassaAlzata = true;
            dialogueManager.StartDialogue(dialogoCassa);
        }

    }
}
