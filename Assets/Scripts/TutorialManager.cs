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
    public Dialogue[] dialogoTutorialTerra;
    public Dialogue[] dialogoTutorialTerraFine;

    public Dialogue[] dialogoFineDemo;


    public bool obiettivoRaggiunto;
    public bool cassaAlzata;
    public bool tutorialTerra;
    public bool tutorialTerraFinito;

    public bool fineDemo;

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

    public void DialogoTutorialTerra()
    {
        if (tutorialTerra == false)
        {
            tutorialTerra = true;
            dialogueManager.StartDialogue(dialogoTutorialTerra);
        }
    }

    public void DialogoTutorialTerraFine()
    {
        if (tutorialTerraFinito == false)
        {
            tutorialTerraFinito = true;
            dialogueManager.StartDialogue(dialogoTutorialTerraFine);


        }
    }

    public void DialogoFineDemo()
    {
        if (fineDemo == false)
        {
            fineDemo = true;
            dialogueManager.StartDialogue(dialogoFineDemo);
        }
    }
}
